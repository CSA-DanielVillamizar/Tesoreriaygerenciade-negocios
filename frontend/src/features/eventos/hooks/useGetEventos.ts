'use client';

import { useQuery } from '@tanstack/react-query';
import { getEventos, type EventoDto } from '@/features/eventos/services/eventosService';

export function useGetEventos() {
    return useQuery<EventoDto[]>({
        queryKey: ['eventos', 'listado'],
        queryFn: getEventos,
        staleTime: 1000 * 60,
    });
}
