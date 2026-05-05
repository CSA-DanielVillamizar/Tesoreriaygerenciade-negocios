'use client';

import { marcarAsistencia, type MarcarAsistenciaPayload } from '@/features/eventos/services/eventosService';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import axios from 'axios';

type ProblemDetails = {
    title?: string;
    detail?: string;
    errors?: Record<string, string[]>;
};

function mapError(error: unknown, fallbackMessage: string): Error {
    if (axios.isAxiosError<ProblemDetails>(error)) {
        const validationErrors = error.response?.data?.errors;
        const firstValidationError = validationErrors
            ? Object.values(validationErrors).flat().find((message) => message)
            : undefined;

        return new Error(
            firstValidationError
            ?? error.response?.data?.detail
            ?? error.response?.data?.title
            ?? fallbackMessage,
        );
    }

    return new Error(fallbackMessage);
}

export function useMarcarAsistencia(eventoId: string) {
    const queryClient = useQueryClient();

    return useMutation<void, Error, MarcarAsistenciaPayload>({
        mutationFn: async (payload) => {
            try {
                await marcarAsistencia(eventoId, payload);
            } catch (error) {
                throw mapError(error, 'No fue posible marcar la asistencia.');
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['eventos', 'detalle', eventoId] });
            await queryClient.invalidateQueries({ queryKey: ['eventos'] });
        },
    });
}
