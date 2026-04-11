'use client';

import {
    getCuentasPorCobrar,
    type CuentaPorCobrarItem,
    type GetCuentasPorCobrarParams,
} from '@/features/cartera/services/carteraService';
import { useQuery } from '@tanstack/react-query';

export function useGetCuentasPorCobrar(params?: GetCuentasPorCobrarParams) {
    return useQuery<CuentaPorCobrarItem[]>({
        queryKey: ['cartera', 'cuentas-por-cobrar', params?.estado ?? null, params?.miembroId ?? null],
        queryFn: () => getCuentasPorCobrar(params),
    });
}
