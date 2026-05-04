'use client';

import { getCajas, type CajaTesoreria } from '@/features/tesoreria/services/tesoreriaService';
import { useQuery } from '@tanstack/react-query';

export function useGetCajas() {
    return useQuery<CajaTesoreria[]>({
        queryKey: ['tesoreria', 'cajas'],
        queryFn: getCajas,
    });
}
