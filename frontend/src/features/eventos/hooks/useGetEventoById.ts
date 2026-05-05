'use client';

import { useQuery } from '@tanstack/react-query';
import { getEventoById, type EventoDetalleDto } from '@/features/eventos/services/eventosService';

export function useGetEventoById(id: string) {
    return useQuery<EventoDetalleDto>({
        queryKey: ['eventos', 'detalle', id],
        queryFn: () => getEventoById(id),
        enabled: Boolean(id),
    });
}
