'use client';

import { getUsuarios, type UsuarioDto } from '@/features/seguridad/services/usuariosService';
import { useQuery } from '@tanstack/react-query';

export function useGetUsuarios() {
    return useQuery<UsuarioDto[]>({
        queryKey: ['seguridad', 'usuarios'],
        queryFn: getUsuarios,
        staleTime: 1000 * 60,
    });
}
