import apiClient from '@/lib/apiClient';

export type EstadoResultadosDetalle = {
    tipoMovimiento: string;
    concepto: string;
    total: number;
};

export type EstadoResultados = {
    totalIngresos: number;
    totalEgresos: number;
    balanceNeto: number;
    totalesPorConcepto: EstadoResultadosDetalle[];
};

export type CarteraMoraDetalle = {
    nombreMiembro: string;
    concepto: string;
    fechaVencimiento: string;
    saldoPendiente: number;
};

export type CarteraMora = {
    totalEnMora: number;
    detalleMora: CarteraMoraDetalle[];
};

type EstadoResultadosApiDto = {
    totalIngresos?: number | string;
    TotalIngresos?: number | string;
    totalEgresos?: number | string;
    TotalEgresos?: number | string;
    balanceNeto?: number | string;
    BalanceNeto?: number | string;
    totalesPorConcepto?: EstadoResultadosDetalleApiDto[];
    TotalesPorConcepto?: EstadoResultadosDetalleApiDto[];
};

type EstadoResultadosDetalleApiDto = {
    tipoMovimiento?: string;
    TipoMovimiento?: string;
    concepto?: string;
    Concepto?: string;
    total?: number | string;
    Total?: number | string;
};

type CarteraMoraApiDto = {
    totalEnMora?: number | string;
    TotalEnMora?: number | string;
    detalleMora?: CarteraMoraDetalleApiDto[];
    DetalleMora?: CarteraMoraDetalleApiDto[];
};

type CarteraMoraDetalleApiDto = {
    nombreMiembro?: string;
    NombreMiembro?: string;
    concepto?: string;
    Concepto?: string;
    fechaVencimiento?: string;
    FechaVencimiento?: string;
    saldoPendiente?: number | string;
    SaldoPendiente?: number | string;
};

function toNumber(value: unknown): number {
    const parsed = typeof value === 'number' ? value : Number(value);
    return Number.isFinite(parsed) ? parsed : 0;
}

export async function getEstadoResultados(fechaInicio: string, fechaFin: string): Promise<EstadoResultados> {
    const response = await apiClient.get<EstadoResultadosApiDto>('/api/reportes/estado-resultados', {
        params: {
            fechaInicio,
            fechaFin,
        },
    });

    const item = response.data ?? {};
    const detalle = (item.totalesPorConcepto ?? item.TotalesPorConcepto ?? []).map((d) => ({
        tipoMovimiento: String(d?.tipoMovimiento ?? d?.TipoMovimiento ?? ''),
        concepto: String(d?.concepto ?? d?.Concepto ?? ''),
        total: toNumber(d?.total ?? d?.Total ?? 0),
    }));

    return {
        totalIngresos: toNumber(item.totalIngresos ?? item.TotalIngresos ?? 0),
        totalEgresos: toNumber(item.totalEgresos ?? item.TotalEgresos ?? 0),
        balanceNeto: toNumber(item.balanceNeto ?? item.BalanceNeto ?? 0),
        totalesPorConcepto: detalle,
    };
}

export async function getCarteraMora(): Promise<CarteraMora> {
    const response = await apiClient.get<CarteraMoraApiDto>('/api/reportes/cartera-mora');

    const item = response.data ?? {};
    const detalle = (item.detalleMora ?? item.DetalleMora ?? []).map((d) => ({
        nombreMiembro: String(d?.nombreMiembro ?? d?.NombreMiembro ?? ''),
        concepto: String(d?.concepto ?? d?.Concepto ?? ''),
        fechaVencimiento: String(d?.fechaVencimiento ?? d?.FechaVencimiento ?? ''),
        saldoPendiente: toNumber(d?.saldoPendiente ?? d?.SaldoPendiente ?? 0),
    }));

    return {
        totalEnMora: toNumber(item.totalEnMora ?? item.TotalEnMora ?? 0),
        detalleMora: detalle,
    };
}
