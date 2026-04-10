'use client';

import {
    getMiembrosLookup,
    type MiembroLookupItem,
} from '@/features/cartera/services/carteraService';
import { useQuery } from '@tanstack/react-query';

export function useGetMiembrosLookup() {
    return useQuery<MiembroLookupItem[]>({
        queryKey: ['cartera', 'lookup', 'miembros'],
        queryFn: getMiembrosLookup,
        staleTime: 1000 * 60 * 5,
    });
}
