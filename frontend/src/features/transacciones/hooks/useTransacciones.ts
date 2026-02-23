import apiClient from '@/lib/apiClient';
import { useQuery } from '@tanstack/react-query';

export type TransaccionItem = {
    id: string;
    fecha: string;
    tipo: 'Ingreso' | 'Egreso' | string;
    montoCOP: number;
    descripcion: string;
    centroCosto: string;
    banco: string;
};

export const useTransacciones = () => {
    return useQuery<TransaccionItem[]>({
        queryKey: ['transacciones', 'listado'],
        queryFn: async () => {
            const response = await apiClient.get<TransaccionItem[]>('/api/transacciones');
            return response.data;
        },
    });
};
