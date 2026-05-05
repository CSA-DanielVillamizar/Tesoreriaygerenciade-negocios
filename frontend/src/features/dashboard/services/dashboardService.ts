import apiClient from '@/lib/apiClient';

export type DashboardResumenDto = {
    totalMiembrosActivos: number;
    totalDineroCajas: number;
    proximoEventoNombre: string | null;
    proximaFechaEvento: string | null;
};

type DashboardResumenApiDto = {
    totalMiembrosActivos?: number | string | null;
    TotalMiembrosActivos?: number | string | null;
    totalDineroCajas?: number | string | null;
    TotalDineroCajas?: number | string | null;
    proximoEventoNombre?: string | null;
    ProximoEventoNombre?: string | null;
    proximaFechaEvento?: string | null;
    ProximaFechaEvento?: string | null;
};

function toNumberValue(value: unknown): number {
    const parsed = typeof value === 'number' ? value : Number(value ?? 0);
    return Number.isFinite(parsed) ? parsed : 0;
}

function toNullableStringValue(value: unknown): string | null {
    if (typeof value !== 'string') {
        return null;
    }

    const trimmed = value.trim();
    return trimmed.length > 0 ? trimmed : null;
}

function mapResumen(item: DashboardResumenApiDto | null | undefined): DashboardResumenDto {
    return {
        totalMiembrosActivos: toNumberValue(item?.totalMiembrosActivos ?? item?.TotalMiembrosActivos),
        totalDineroCajas: toNumberValue(item?.totalDineroCajas ?? item?.TotalDineroCajas),
        proximoEventoNombre: toNullableStringValue(item?.proximoEventoNombre ?? item?.ProximoEventoNombre),
        proximaFechaEvento: toNullableStringValue(item?.proximaFechaEvento ?? item?.ProximaFechaEvento),
    };
}

export async function getResumenDashboard(): Promise<DashboardResumenDto> {
    const response = await apiClient.get<DashboardResumenApiDto>('/api/dashboard/resumen');
    return mapResumen(response.data);
}
