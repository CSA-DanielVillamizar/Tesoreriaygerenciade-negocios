import {
    registrarVenta,
    type RegistrarVentaPayload,
} from '@/features/merchandising/services/merchandisingService';
import { useMutation, useQueryClient } from '@tanstack/react-query';

type RegistrarVentaParams = {
    productoId: string;
    payload: RegistrarVentaPayload;
};

export function useRegistrarVenta() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ productoId, payload }: RegistrarVentaParams) =>
            registrarVenta(productoId, payload),
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['merchandising', 'productos'] });
        },
    });
}
