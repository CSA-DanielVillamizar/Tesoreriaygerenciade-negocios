'use client';

import {
    getConceptosCobroLookup,
    type ConceptoCobroLookupItem,
} from '@/features/cartera/services/carteraService';
import { useQuery } from '@tanstack/react-query';

export function useGetConceptosCobroLookup() {
    return useQuery<ConceptoCobroLookupItem[]>({
        queryKey: ['cartera', 'lookup', 'conceptos-cobro'],
        queryFn: getConceptosCobroLookup,
        staleTime: 1000 * 60 * 5,
    });
}
