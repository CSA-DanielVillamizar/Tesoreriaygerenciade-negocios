'use client';

import { getResumenDashboard, type DashboardResumenDto } from '@/features/dashboard/services/dashboardService';
import { useQuery } from '@tanstack/react-query';

export function useGetResumenDashboard() {
    return useQuery<DashboardResumenDto>({
        queryKey: ['dashboard', 'resumen-general'],
        queryFn: getResumenDashboard,
        staleTime: 1000 * 30,
    });
}
