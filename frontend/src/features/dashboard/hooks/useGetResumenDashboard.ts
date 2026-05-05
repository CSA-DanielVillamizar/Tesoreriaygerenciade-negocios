'use client';

import { useQuery } from '@tanstack/react-query';
import { getResumenDashboard, type DashboardResumenDto } from '@/features/dashboard/services/dashboardService';

export function useGetResumenDashboard() {
    return useQuery<DashboardResumenDto>({
        queryKey: ['dashboard', 'resumen-general'],
        queryFn: getResumenDashboard,
        staleTime: 1000 * 30,
    });
}
