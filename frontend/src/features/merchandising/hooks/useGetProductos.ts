import { getProductos } from '@/features/merchandising/services/merchandisingService';
import { useQuery } from '@tanstack/react-query';

export function useGetProductos() {
    return useQuery({
        queryKey: ['merchandising', 'productos'],
        queryFn: getProductos,
    });
}
