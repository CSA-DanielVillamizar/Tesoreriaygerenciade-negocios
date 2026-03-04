'use client';

import { useQuery } from '@tanstack/react-query';
import apiClient from '@/lib/apiClient';

export type ValorizacionInventario = {
    totalArticulosActivos: number;
    stockTotal: number;
    valorTotalInventario: number;
};

export type ResumenVentasUtilidad = {
    totalVendido: number;
    costoTotalVendido: number;
    utilidadNeta: number;
    cantidadVentas: number;
};

type ResumenVentasFilters = {
    fechaInicio?: string;
    fechaFin?: string;
};

export function useValorizacionInventario() {
    return useQuery<ValorizacionInventario>({
        queryKey: ['merchandising', 'reportes', 'valorizacion-inventario'],
        queryFn: async () => {
            const response = await apiClient.get<any>('/api/merchandising/reportes/valorizacion-inventario');
            const item = response.data ?? {};

            return {
                totalArticulosActivos: Number(item?.totalArticulosActivos ?? item?.TotalArticulosActivos ?? 0),
                stockTotal: Number(item?.stockTotal ?? item?.StockTotal ?? 0),
                valorTotalInventario: Number(item?.valorTotalInventario ?? item?.ValorTotalInventario ?? 0),
            };
        },
    });
}

export function useResumenVentasUtilidad(filters: ResumenVentasFilters) {
    return useQuery<ResumenVentasUtilidad>({
        queryKey: ['merchandising', 'reportes', 'resumen-ventas-utilidad', filters.fechaInicio ?? '', filters.fechaFin ?? ''],
        queryFn: async () => {
            const params: Record<string, string> = {};
            if (filters.fechaInicio) {
                params.fechaInicio = filters.fechaInicio;
            }
            if (filters.fechaFin) {
                params.fechaFin = filters.fechaFin;
            }

            const response = await apiClient.get<any>('/api/merchandising/reportes/resumen-ventas-utilidad', { params });
            const item = response.data ?? {};

            return {
                totalVendido: Number(item?.totalVendido ?? item?.TotalVendido ?? 0),
                costoTotalVendido: Number(item?.costoTotalVendido ?? item?.CostoTotalVendido ?? 0),
                utilidadNeta: Number(item?.utilidadNeta ?? item?.UtilidadNeta ?? 0),
                cantidadVentas: Number(item?.cantidadVentas ?? item?.CantidadVentas ?? 0),
            };
        },
    });
}
