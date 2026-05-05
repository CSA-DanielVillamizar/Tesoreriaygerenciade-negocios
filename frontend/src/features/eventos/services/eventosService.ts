import apiClient from '@/lib/apiClient';

export type EventoDto = {
    id: string;
    nombre: string;
    fechaProgramada: string;
    tipoEvento: string;
    estado: string;
};

export type CreateEventoPayload = {
    nombre: string;
    descripcion: string;
    fechaProgramada: string;
    lugarEncuentro: string;
    destino?: string | null;
    tipoEvento: number;
};

type IdResponseDto = {
    id?: string;
    Id?: string;
};

function toStringValue(value: unknown): string {
    return typeof value === 'string' ? value : String(value ?? '');
}

function mapEvento(item: unknown): EventoDto {
    const dto = (item ?? {}) as Record<string, unknown>;

    return {
        id: toStringValue(dto.id ?? dto.Id),
        nombre: toStringValue(dto.nombre ?? dto.Nombre),
        fechaProgramada: toStringValue(dto.fechaProgramada ?? dto.FechaProgramada),
        tipoEvento: toStringValue(dto.tipoEvento ?? dto.TipoEvento),
        estado: toStringValue(dto.estado ?? dto.Estado),
    };
}

export async function getEventos(): Promise<EventoDto[]> {
    const response = await apiClient.get<unknown[]>('/api/eventos');
    return (response.data ?? []).map(mapEvento).filter((evento) => evento.id.length > 0);
}

export async function crearEvento(payload: CreateEventoPayload): Promise<{ id: string }> {
    const response = await apiClient.post<IdResponseDto>('/api/eventos', payload);
    return { id: toStringValue(response.data?.id ?? response.data?.Id) };
}
