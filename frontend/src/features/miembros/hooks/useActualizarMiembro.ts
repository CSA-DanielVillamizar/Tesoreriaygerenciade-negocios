'use client';

import { actualizarMiembro, type ActualizarMiembroPayload } from '@/features/miembros/services/miembrosService';
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

export function useActualizarMiembro() {
    const queryClient = useQueryClient();

    return useMutation<void, Error, { id: string; payload: ActualizarMiembroPayload }>({
        mutationFn: async ({ id, payload }) => {
            try {
                await actualizarMiembro(id, payload);
            } catch (error) {
                throw mapError(error, 'No fue posible actualizar el miembro.');
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['miembros'] });
        },
    });
}
