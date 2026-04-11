'use client';

import ListaCuentasPorCobrar from '@/features/cartera/components/ListaCuentasPorCobrar';
import { useGetCuentasPorCobrar } from '@/features/cartera/hooks/useGetCuentasPorCobrar';
import { useGetMiembrosLookup } from '@/features/cartera/hooks/useGetMiembrosLookup';
import { useState } from 'react';

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    }).format(value);
}

const ESTADOS = [
    { value: '', label: 'Todos los estados' },
    { value: '1', label: 'Pendiente' },
    { value: '2', label: 'Pagada Parcial' },
    { value: '3', label: 'Pagada' },
    { value: '4', label: 'Anulada' },
] as const;

export default function CarteraDashboardPage() {
    const [estadoFiltro, setEstadoFiltro] = useState<number | undefined>(undefined);
    const [miembroIdFiltro, setMiembroIdFiltro] = useState<string | undefined>(undefined);

    const { data: miembros = [], isLoading: isLoadingMiembros } = useGetMiembrosLookup();

    const { data: cuentas = [], isLoading, error } = useGetCuentasPorCobrar({
        estado: estadoFiltro,
        miembroId: miembroIdFiltro,
    });

    const totalSaldoPendiente = cuentas.reduce((acc, item) => acc + item.saldoPendiente, 0);

    return (
        <main className="mx-auto max-w-7xl px-6 py-10">
            <header className="mb-6">
                <h1 className="text-2xl font-semibold text-slate-900">Cartera - Cuentas por Cobrar</h1>
                <p className="mt-1 text-sm text-slate-600">
                    Visualización general de cuentas por cobrar con su saldo pendiente y estado.
                </p>
            </header>

            <section className="mb-6 rounded-xl border border-amber-200 bg-amber-50 p-4">
                <p className="text-sm text-amber-800">Saldo pendiente total (vista actual)</p>
                <p className="mt-1 text-2xl font-bold text-amber-900">
                    {isLoading ? 'Cargando...' : formatCOP(totalSaldoPendiente)}
                </p>
            </section>

            {/* Filtros */}
            <section className="mb-4 flex flex-wrap gap-4">
                <div className="flex flex-col gap-1">
                    <label htmlFor="filtro-estado" className="text-xs font-medium text-slate-600">
                        Estado
                    </label>
                    <select
                        id="filtro-estado"
                        className="rounded-md border border-slate-300 bg-white px-3 py-2 text-sm text-slate-800 shadow-sm focus:outline-none focus:ring-2 focus:ring-amber-400"
                        value={estadoFiltro ?? ''}
                        onChange={(e) => {
                            const val = e.target.value;
                            setEstadoFiltro(val === '' ? undefined : Number(val));
                        }}
                    >
                        {ESTADOS.map((op) => (
                            <option key={op.value} value={op.value}>
                                {op.label}
                            </option>
                        ))}
                    </select>
                </div>

                <div className="flex flex-col gap-1">
                    <label htmlFor="filtro-miembro" className="text-xs font-medium text-slate-600">
                        Miembro
                    </label>
                    <select
                        id="filtro-miembro"
                        className="rounded-md border border-slate-300 bg-white px-3 py-2 text-sm text-slate-800 shadow-sm focus:outline-none focus:ring-2 focus:ring-amber-400 disabled:opacity-50"
                        value={miembroIdFiltro ?? ''}
                        disabled={isLoadingMiembros}
                        onChange={(e) => {
                            const val = e.target.value;
                            setMiembroIdFiltro(val === '' ? undefined : val);
                        }}
                    >
                        <option value="">
                            {isLoadingMiembros ? 'Cargando...' : 'Todos los miembros'}
                        </option>
                        {miembros.map((m) => (
                            <option key={m.id} value={m.id}>
                                {m.nombreCompleto}
                            </option>
                        ))}
                    </select>
                </div>
            </section>

            <ListaCuentasPorCobrar
                cuentas={cuentas}
                isLoading={isLoading}
                error={error instanceof Error ? error : null}
            />
        </main>
    );
}
