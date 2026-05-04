import apiClient from '@/lib/apiClient';

export type CajaTesoreria = {
    id: string;
    nombre: string;
    tipoCaja: number;
    cuentaContable: string;
    saldoActual: number;
};

export type RegistrarMovimientoTesoreriaPayload = {
    monto: number;
    concepto: string;
    cuentaContableId: string;
    cajaId: string;
    centroCostoId: string;
    fecha?: string;
    terceroId?: string | null;
};

type RegistrarMovimientoTesoreriaResponse = {
    id: string;
};

type CajaApiDto = {
    id?: string;
    Id?: string;
    nombre?: string;
    Nombre?: string;
    tipoCaja?: number;
    TipoCaja?: number;
    cuentaContable?: string;
    CuentaContable?: string;
    saldoActual?: number;
    SaldoActual?: number;
};

type RegistrarMovimientoTesoreriaResponseDto = {
    id?: string;
    Id?: string;
};

export async function getCajas(): Promise<CajaTesoreria[]> {
    const response = await apiClient.get<CajaApiDto[]>('/api/tesoreria/cajas');

    return (response.data ?? []).map((item) => ({
        id: String(item?.id ?? item?.Id ?? ''),
        nombre: String(item?.nombre ?? item?.Nombre ?? ''),
        tipoCaja: Number(item?.tipoCaja ?? item?.TipoCaja ?? 0),
        cuentaContable: String(item?.cuentaContable ?? item?.CuentaContable ?? ''),
        saldoActual: Number(item?.saldoActual ?? item?.SaldoActual ?? 0),
    }));
}

export async function registrarIngreso(payload: RegistrarMovimientoTesoreriaPayload): Promise<RegistrarMovimientoTesoreriaResponse> {
    const response = await apiClient.post<RegistrarMovimientoTesoreriaResponseDto>('/api/tesoreria/ingresos', payload);

    return {
        id: String(response.data?.id ?? response.data?.Id ?? ''),
    };
}

export async function registrarEgreso(payload: RegistrarMovimientoTesoreriaPayload): Promise<RegistrarMovimientoTesoreriaResponse> {
    const response = await apiClient.post<RegistrarMovimientoTesoreriaResponseDto>('/api/tesoreria/egresos', payload);

    return {
        id: String(response.data?.id ?? response.data?.Id ?? ''),
    };
}
