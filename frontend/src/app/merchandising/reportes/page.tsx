'use client';

import { useMemo, useState } from 'react';
import { useResumenVentasUtilidad, useValorizacionInventario } from '@/features/merchandising/hooks/useReportesMerchandising';

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        maximumFractionDigits: 0,
    }).format(value);
}

type KpiCardProps = {
    title: string;
    value: string;
    subtitle?: string;
};

function KpiCard({ title, value, subtitle }: KpiCardProps) {
    return (
        <article className="rounded-xl border border-slate-200 bg-white p-5 shadow-sm">
            <p className="text-sm font-medium text-slate-600">{title}</p>
            <p className="mt-2 text-2xl font-bold text-slate-900">{value}</p>
            {subtitle ? <p className="mt-1 text-xs text-slate-500">{subtitle}</p> : null}
        </article>
    );
}

export default function ReportesMerchandisingPage() {
    const [fechaInicio, setFechaInicio] = useState('');
    const [fechaFin, setFechaFin] = useState('');

    const valorizacionQuery = useValorizacionInventario();
    const resumenVentasQuery = useResumenVentasUtilidad({ fechaInicio, fechaFin });

    const filtroDescripcion = useMemo(() => {
        if (!fechaInicio && !fechaFin) {
            return 'Todo el histórico';
        }

        const inicio = fechaInicio || '...';
        const fin = fechaFin || '...';
        return `Desde ${inicio} hasta ${fin}`;
    }, [fechaInicio, fechaFin]);

    return (
        <main className="min-h-screen bg-slate-50 px-6 py-10">
            <div className="mx-auto flex w-full max-w-6xl flex-col gap-6">
                <header>
                    <h1 className="text-3xl font-bold text-slate-900">Reportes de Merchandising</h1>
                    <p className="mt-1 text-slate-600">Valorización de inventario y resumen de ventas/utilidad</p>
                </header>

                <section className="grid grid-cols-1 gap-4 md:grid-cols-2">
                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">Fecha inicio (opcional)</label>
                        <input
                            type="date"
                            value={fechaInicio}
                            onChange={(event) => setFechaInicio(event.target.value)}
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>
                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">Fecha fin (opcional)</label>
                        <input
                            type="date"
                            value={fechaFin}
                            onChange={(event) => setFechaFin(event.target.value)}
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>
                </section>

                {valorizacionQuery.isError || resumenVentasQuery.isError ? (
                    <section className="rounded-xl border border-red-200 bg-red-50 p-4 text-sm text-red-700 shadow-sm">
                        {(valorizacionQuery.error as Error | null)?.message ?? (resumenVentasQuery.error as Error | null)?.message ?? 'No fue posible cargar los reportes.'}
                    </section>
                ) : null}

                <section className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
                    <KpiCard
                        title="Valor Total Inventario"
                        value={formatCOP(valorizacionQuery.data?.valorTotalInventario ?? 0)}
                        subtitle={`Artículos activos: ${valorizacionQuery.data?.totalArticulosActivos ?? 0}`}
                    />
                    <KpiCard
                        title="Stock Total"
                        value={String(valorizacionQuery.data?.stockTotal ?? 0)}
                        subtitle="Unidades disponibles"
                    />
                    <KpiCard
                        title="Total Vendido"
                        value={formatCOP(resumenVentasQuery.data?.totalVendido ?? 0)}
                        subtitle={filtroDescripcion}
                    />
                    <KpiCard
                        title="Costo Total Vendido"
                        value={formatCOP(resumenVentasQuery.data?.costoTotalVendido ?? 0)}
                        subtitle={`Ventas procesadas: ${resumenVentasQuery.data?.cantidadVentas ?? 0}`}
                    />
                    <KpiCard
                        title="Utilidad Neta"
                        value={formatCOP(resumenVentasQuery.data?.utilidadNeta ?? 0)}
                        subtitle="Total vendido - costo total"
                    />
                </section>
            </div>
        </main>
    );
}
