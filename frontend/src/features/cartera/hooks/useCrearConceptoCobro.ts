'use client';

import type { CrearConceptoCobroPayload } from '@/features/cartera/services/carteraService';
import { crearConceptoCobro } from '@/features/cartera/services/carteraService';
import { useMutation } from '@tanstack/react-query';
import axios from 'axios';

type ProblemDetails = {
    title?: string;
    detail?: string;
    errors?: Record<string, string[]>;
};

type UseCrearConceptoCobroOptions = {
    onSuccessNotification?: (message: string) => void;
    onErrorNotification?: (message: string) => void;
};

function getErrorMessage(error: unknown): string {
    if (!axios.isAxiosError<ProblemDetails>(error)) {
        return 'No fue posible crear el concepto de cobro.';
    }

    const validationErrors = error.response?.data?.errors;
    const firstValidationError = validationErrors
        ? Object.values(validationErrors).flat().find((message) => message)
        : undefined;

    return firstValidationError
        ?? error.response?.data?.detail
        ?? error.response?.data?.title
        ?? 'No fue posible crear el concepto de cobro.';
}

export function useCrearConceptoCobro(options?: UseCrearConceptoCobroOptions) {
    return useMutation<{ id: string }, Error, CrearConceptoCobroPayload>({
        mutationFn: async (payload) => {
            try {
                return await crearConceptoCobro(payload);
            } catch (error) {
                throw new Error(getErrorMessage(error));
            }
        },
        onSuccess: () => {
            options?.onSuccessNotification?.('Concepto de cobro creado correctamente.');
        },
        onError: (error) => {
            options?.onErrorNotification?.(error.message);
        },
    });
}
