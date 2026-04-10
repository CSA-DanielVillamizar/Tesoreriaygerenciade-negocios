import apiClient from '@/lib/apiClient';

export type CrearMiembroPayload = {
    documentoIdentidad: string;
    nombres: string;
    apellidos: string;
    apodo: string;
    fechaIngreso: string;
    tipoMiembro: number;
};

export type CrearConceptoCobroPayload = {
    nombre: string;
    valorCOP: number;
    periodicidadMensual: number;
    cuentaContableIngresoId: string;
};

export type CrearCuentaPorCobrarPayload = {
    miembroId: string;
    conceptoCobroId: string;
    fechaEmision: string;
    fechaVencimiento: string;
    valorTotal: number;
};

export type MiembroLookupItem = {
    id: string;
    nombreCompleto: string;
};

export type ConceptoCobroLookupItem = {
    id: string;
    nombre: string;
};

type IdResponseDto = {
    id?: string;
    Id?: string;
};

function toId(response: IdResponseDto | undefined): string {
    return String(response?.id ?? response?.Id ?? '');
}

export async function crearMiembro(payload: CrearMiembroPayload): Promise<{ id: string }> {
    const response = await apiClient.post<IdResponseDto>('/api/cartera/miembros', payload);
    return { id: toId(response.data) };
}

export async function crearConceptoCobro(payload: CrearConceptoCobroPayload): Promise<{ id: string }> {
    const response = await apiClient.post<IdResponseDto>('/api/cartera/conceptos-cobro', payload);
    return { id: toId(response.data) };
}

export async function crearCuentaPorCobrar(payload: CrearCuentaPorCobrarPayload): Promise<{ id: string }> {
    const response = await apiClient.post<IdResponseDto>('/api/cartera/cuentas-por-cobrar', payload);
    return { id: toId(response.data) };
}

export async function getMiembrosLookup(): Promise<MiembroLookupItem[]> {
    const response = await apiClient.get<any[]>('/api/cartera/miembros/lookup');

    return (response.data ?? []).map((item) => ({
        id: String(item?.id ?? item?.Id ?? ''),
        nombreCompleto: String(item?.nombreCompleto ?? item?.NombreCompleto ?? ''),
    }));
}

export async function getConceptosCobroLookup(): Promise<ConceptoCobroLookupItem[]> {
    const response = await apiClient.get<any[]>('/api/cartera/conceptos-cobro/lookup');

    return (response.data ?? []).map((item) => ({
        id: String(item?.id ?? item?.Id ?? ''),
        nombre: String(item?.nombre ?? item?.Nombre ?? ''),
    }));
}
