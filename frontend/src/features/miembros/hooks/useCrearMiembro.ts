'use client';

import { crearMiembro, type CrearMiembroPayload } from '@/features/miembros/services/miembrosService';
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

export function useCrearMiembro() {
    const queryClient = useQueryClient();

    return useMutation<{ id: string }, Error, CrearMiembroPayload>({
        mutationFn: async (payload) => {
            try {
                return await crearMiembro(payload);
            } catch (error) {
                throw mapError(error, 'No fue posible crear el miembro.');
            }
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['miembros'] });
        },
    });
}
