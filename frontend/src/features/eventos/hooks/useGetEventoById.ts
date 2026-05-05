'use client';

import { getEventoById, type EventoDetalleDto } from '@/features/eventos/services/eventosService';
import { useQuery } from '@tanstack/react-query';

export function useGetEventoById(id: string) {
    return useQuery<EventoDetalleDto>({
        queryKey: ['eventos', 'detalle', id],
        queryFn: () => getEventoById(id),
        enabled: Boolean(id),
    });
}
