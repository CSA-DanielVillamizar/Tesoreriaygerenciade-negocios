'use client';

import type { CrearCuentaPorCobrarPayload } from '@/features/cartera/services/carteraService';
import { crearCuentaPorCobrar } from '@/features/cartera/services/carteraService';
import { useMutation } from '@tanstack/react-query';
import axios from 'axios';

type ProblemDetails = {
    title?: string;
    detail?: string;
    errors?: Record<string, string[]>;
};

type UseCrearCuentaPorCobrarOptions = {
    onSuccessNotification?: (message: string) => void;
    onErrorNotification?: (message: string) => void;
};

function getErrorMessage(error: unknown): string {
    if (!axios.isAxiosError<ProblemDetails>(error)) {
        return 'No fue posible crear la cuenta por cobrar.';
    }

    const validationErrors = error.response?.data?.errors;
    const firstValidationError = validationErrors
        ? Object.values(validationErrors).flat().find((message) => message)
        : undefined;

    return firstValidationError
        ?? error.response?.data?.detail
        ?? error.response?.data?.title
        ?? 'No fue posible crear la cuenta por cobrar.';
}

export function useCrearCuentaPorCobrar(options?: UseCrearCuentaPorCobrarOptions) {
    return useMutation<{ id: string }, Error, CrearCuentaPorCobrarPayload>({
        mutationFn: async (payload) => {
            try {
                return await crearCuentaPorCobrar(payload);
            } catch (error) {
                throw new Error(getErrorMessage(error));
            }
        },
        onSuccess: () => {
            options?.onSuccessNotification?.('Cuenta por cobrar creada correctamente.');
        },
        onError: (error) => {
            options?.onErrorNotification?.(error.message);
        },
    });
}
