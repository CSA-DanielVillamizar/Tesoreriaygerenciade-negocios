'use client';

import type { CrearMiembroPayload } from '@/features/cartera/services/carteraService';
import { crearMiembro } from '@/features/cartera/services/carteraService';
import { useMutation } from '@tanstack/react-query';
import axios from 'axios';

type ProblemDetails = {
    title?: string;
    detail?: string;
    errors?: Record<string, string[]>;
};

type UseCrearMiembroOptions = {
    onSuccessNotification?: (message: string) => void;
    onErrorNotification?: (message: string) => void;
};

function getErrorMessage(error: unknown): string {
    if (!axios.isAxiosError<ProblemDetails>(error)) {
        return 'No fue posible crear el miembro.';
    }

    const validationErrors = error.response?.data?.errors;
    const firstValidationError = validationErrors
        ? Object.values(validationErrors).flat().find((message) => message)
        : undefined;

    return firstValidationError
        ?? error.response?.data?.detail
        ?? error.response?.data?.title
        ?? 'No fue posible crear el miembro.';
}

export function useCrearMiembro(options?: UseCrearMiembroOptions) {
    return useMutation<{ id: string }, Error, CrearMiembroPayload>({
        mutationFn: async (payload) => {
            try {
                return await crearMiembro(payload);
            } catch (error) {
                throw new Error(getErrorMessage(error));
            }
        },
        onSuccess: () => {
            options?.onSuccessNotification?.('Miembro creado correctamente.');
        },
        onError: (error) => {
            options?.onErrorNotification?.(error.message);
        },
    });
}
