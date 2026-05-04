'use client';

import { useGetCarteraMora } from '@/features/reportes/hooks/useGetCarteraMora';
import { exportToCSV } from '@/lib/exportUtils';

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        maximumFractionDigits: 0,
    }).format(value);
}

function formatFecha(value: string): string {
    if (!value) {
        return 'N/A';
    }

    const parsed = new Date(value);
    if (Number.isNaN(parsed.getTime())) {
        return value;
    }

    return new Intl.DateTimeFormat('es-CO', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
    }).format(parsed);
}

export default function CarteraMoraWidget() {
    const query = useGetCarteraMora();

    const onExportarCsv = () => {
        exportToCSV(query.data?.detalleMora ?? [], 'cartera_en_mora.csv');
    };

    return (
        <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
            <header className="mb-5">
                <h2 className="text-2xl font-bold text-slate-900">Cartera en Mora</h2>
                <p className="mt-1 text-sm text-slate-600">Cuentas vencidas con saldo pendiente mayor a cero.</p>
            </header>

            <article className="mb-5 rounded-xl border border-red-200 bg-red-50 p-5">
                <p className="text-sm font-medium text-red-700">Total en mora</p>
                <p className="mt-2 text-3xl font-bold text-red-700">{formatCOP(query.data?.totalEnMora ?? 0)}</p>
            </article>

            {query.isError ? (
                <div className="mb-4 rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
                    {(query.error as Error | null)?.message ?? 'No fue posible cargar la cartera en mora.'}
                </div>
            ) : null}

            <div className="mb-3 flex flex-wrap items-center justify-between gap-3">
                <h3 className="text-lg font-semibold text-slate-900">Detalle de mora</h3>
                <button
                    type="button"
                    onClick={onExportarCsv}
                    disabled={(query.data?.detalleMora.length ?? 0) === 0}
                    className="rounded-lg border border-slate-300 bg-white px-3 py-2 text-sm font-medium text-slate-700 disabled:cursor-not-allowed disabled:opacity-50"
                >
                    Exportar a CSV
                </button>
            </div>

            <div className="overflow-x-auto rounded-lg border border-slate-200">
                <table className="min-w-full divide-y divide-slate-200 text-sm">
                    <thead className="bg-slate-50">
                        <tr>
                            <th className="px-3 py-2 text-left font-semibold text-slate-700">Miembro</th>
                            <th className="px-3 py-2 text-left font-semibold text-slate-700">Concepto</th>
                            <th className="px-3 py-2 text-left font-semibold text-slate-700">Fecha Vencimiento</th>
                            <th className="px-3 py-2 text-right font-semibold text-slate-700">Saldo</th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-slate-100 bg-white">
                        {query.isLoading ? (
                            <tr>
                                <td className="px-3 py-3 text-slate-500" colSpan={4}>Cargando detalle...</td>
                            </tr>
                        ) : (query.data?.detalleMora.length ?? 0) === 0 ? (
                            <tr>
                                <td className="px-3 py-3 text-slate-500" colSpan={4}>No hay cuentas en mora.</td>
                            </tr>
                        ) : (
                            (query.data?.detalleMora ?? []).map((row, index) => (
                                <tr key={`${row.nombreMiembro}-${row.concepto}-${index}`}>
                                    <td className="px-3 py-2 text-slate-800">{row.nombreMiembro}</td>
                                    <td className="px-3 py-2 text-slate-700">{row.concepto}</td>
                                    <td className="px-3 py-2 text-slate-700">{formatFecha(row.fechaVencimiento)}</td>
                                    <td className="px-3 py-2 text-right font-semibold text-red-700">{formatCOP(row.saldoPendiente)}</td>
                                </tr>
                            ))
                        )}
                    </tbody>
                </table>
            </div>
        </section>
    );
}
