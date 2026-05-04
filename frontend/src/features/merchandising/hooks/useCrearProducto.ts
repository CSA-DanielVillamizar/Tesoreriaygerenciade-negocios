import {
    crearProducto,
    type CrearProductoPayload,
} from '@/features/merchandising/services/merchandisingService';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export function useCrearProducto() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (payload: CrearProductoPayload) => crearProducto(payload),
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['merchandising', 'productos'] });
        },
    });
}
