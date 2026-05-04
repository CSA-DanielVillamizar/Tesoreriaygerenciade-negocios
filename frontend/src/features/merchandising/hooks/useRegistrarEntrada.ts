import {
    registrarEntrada,
    type RegistrarEntradaPayload,
} from '@/features/merchandising/services/merchandisingService';
import { useMutation, useQueryClient } from '@tanstack/react-query';

type RegistrarEntradaParams = {
    productoId: string;
    payload: RegistrarEntradaPayload;
};

export function useRegistrarEntrada() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ productoId, payload }: RegistrarEntradaParams) =>
            registrarEntrada(productoId, payload),
        onSettled: async () => {
            await queryClient.invalidateQueries({ queryKey: ['merchandising', 'productos'] });
        },
    });
}
