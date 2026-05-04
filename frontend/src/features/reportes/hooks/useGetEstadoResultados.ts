'use client';

import { getEstadoResultados, type EstadoResultados } from '@/features/reportes/services/reportesService';
import { useQuery } from '@tanstack/react-query';

export type EstadoResultadosFilters = {
    fechaInicio?: string;
    fechaFin?: string;
};

export type EstadoResultadosDateRange = {
    fechaInicio: string;
    fechaFin: string;
};

function toDateInputValue(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}

export function getDefaultCurrentMonthRange(): EstadoResultadosDateRange {
    const now = new Date();
    const start = new Date(now.getFullYear(), now.getMonth(), 1);
    const end = new Date(now.getFullYear(), now.getMonth() + 1, 0);

    return {
        fechaInicio: toDateInputValue(start),
        fechaFin: toDateInputValue(end),
    };
}

export function useGetEstadoResultados(filters?: EstadoResultadosFilters) {
    const defaultRange = getDefaultCurrentMonthRange();
    const fechaInicio = filters?.fechaInicio?.trim() || defaultRange.fechaInicio;
    const fechaFin = filters?.fechaFin?.trim() || defaultRange.fechaFin;

    return useQuery<EstadoResultados>({
        queryKey: ['reportes', 'estado-resultados', fechaInicio, fechaFin],
        queryFn: () => getEstadoResultados(fechaInicio, fechaFin),
    });
}
