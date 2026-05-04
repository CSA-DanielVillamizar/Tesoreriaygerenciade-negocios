'use client';

import { getDefaultCurrentMonthRange, useGetEstadoResultados } from '@/features/reportes/hooks/useGetEstadoResultados';
import { exportToCSV } from '@/lib/exportUtils';
import { useMemo, useState } from 'react';

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
    cardClassName: string;
    valueClassName: string;
};

function KpiCard({ title, value, cardClassName, valueClassName }: KpiCardProps) {
    return (
        <article className={`rounded-xl border p-5 shadow-sm ${cardClassName}`}>
            <p className="text-sm font-medium text-slate-700">{title}</p>
            <p className={`mt-2 text-3xl font-bold ${valueClassName}`}>{value}</p>
        </article>
    );
}

export default function EstadoResultadosWidget() {
    const defaultRange = useMemo(() => getDefaultCurrentMonthRange(), []);
    const [fechaInicio, setFechaInicio] = useState(defaultRange.fechaInicio);
    const [fechaFin, setFechaFin] = useState(defaultRange.fechaFin);

    const query = useGetEstadoResultados({ fechaInicio, fechaFin });

    const onExportarCsv = () => {
        exportToCSV(query.data?.totalesPorConcepto ?? [], 'estado_resultados.csv');
    };

    return (
        <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
            <header className="mb-5">
                <h2 className="text-2xl font-bold text-slate-900">Estado de Resultados</h2>
                <p className="mt-1 text-sm text-slate-600">Consulta de ingresos, egresos y balance neto por rango de fechas.</p>
            </header>

            <div className="mb-5 grid grid-cols-1 gap-4 md:grid-cols-2">
                <div>
                    <label className="mb-1 block text-sm font-medium text-slate-700">Fecha inicio</label>
                    <input
                        type="date"
                        value={fechaInicio}
                        onChange={(event) => setFechaInicio(event.target.value)}
                        className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                    />
                </div>
                <div>
                    <label className="mb-1 block text-sm font-medium text-slate-700">Fecha fin</label>
                    <input
                        type="date"
                        value={fechaFin}
                        onChange={(event) => setFechaFin(event.target.value)}
                        className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                    />
                </div>
            </div>

            {query.isError ? (
                <div className="mb-4 rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
                    {(query.error as Error | null)?.message ?? 'No fue posible cargar el estado de resultados.'}
                </div>
            ) : null}

            <div className="grid grid-cols-1 gap-4 xl:grid-cols-3">
                <KpiCard
                    title="Total Ingresos"
                    value={formatCOP(query.data?.totalIngresos ?? 0)}
                    cardClassName="border-emerald-200 bg-emerald-50"
                    valueClassName="text-emerald-700"
                />
                <KpiCard
                    title="Total Egresos"
                    value={formatCOP(query.data?.totalEgresos ?? 0)}
                    cardClassName="border-red-200 bg-red-50"
                    valueClassName="text-red-700"
                />
                <KpiCard
                    title="Balance Neto"
                    value={formatCOP(query.data?.balanceNeto ?? 0)}
                    cardClassName="border-slate-200 bg-slate-50"
                    valueClassName="text-slate-900"
                />
            </div>

            <div className="mt-6">
                <div className="mb-2 flex flex-wrap items-center justify-between gap-3">
                    <h3 className="text-lg font-semibold text-slate-900">Totales por concepto</h3>
                    <button
                        type="button"
                        onClick={onExportarCsv}
                        disabled={(query.data?.totalesPorConcepto.length ?? 0) === 0}
                        className="rounded-lg border border-slate-300 bg-white px-3 py-2 text-sm font-medium text-slate-700 disabled:cursor-not-allowed disabled:opacity-50"
                    >
                        Exportar a CSV
                    </button>
                </div>
                {query.isLoading ? (
                    <p className="text-sm text-slate-500">Cargando totales...</p>
                ) : (query.data?.totalesPorConcepto.length ?? 0) === 0 ? (
                    <p className="text-sm text-slate-500">No hay movimientos para el rango seleccionado.</p>
                ) : (
                    <ul className="space-y-2">
                        {(query.data?.totalesPorConcepto ?? []).map((item, index) => (
                            <li
                                key={`${item.tipoMovimiento}-${item.concepto}-${index}`}
                                className="flex items-center justify-between rounded-lg border border-slate-200 bg-slate-50 px-3 py-2"
                            >
                                <div>
                                    <p className="text-sm font-medium text-slate-900">{item.concepto || 'Sin concepto'}</p>
                                    <p className="text-xs text-slate-500">{item.tipoMovimiento}</p>
                                </div>
                                <p className="text-sm font-semibold text-slate-900">{formatCOP(item.total)}</p>
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </section>
    );
}
