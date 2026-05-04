'use client';

import { getCarteraMora, type CarteraMora } from '@/features/reportes/services/reportesService';
import { useQuery } from '@tanstack/react-query';

export function useGetCarteraMora() {
    return useQuery<CarteraMora>({
        queryKey: ['reportes', 'cartera-mora'],
        queryFn: getCarteraMora,
    });
}
