'use client';

import RegistrarEgresoForm from '@/features/tesoreria/components/RegistrarEgresoForm';
import RegistrarIngresoForm from '@/features/tesoreria/components/RegistrarIngresoForm';
import { useGetCajas } from '@/features/tesoreria/hooks/useGetCajas';
import { useState } from 'react';

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    }).format(value);
}

function getTipoCajaLabel(value: number): string {
    return value === 1 ? 'Bancos' : 'Efectivo';
}

export default function ListaCajas() {
    const cajasQuery = useGetCajas();
    const [modalActivo, setModalActivo] = useState<'ingreso' | 'egreso' | null>(null);

    const totalSaldo = (cajasQuery.data ?? []).reduce((sum, caja) => sum + caja.saldoActual, 0);

    return (
        <section className="space-y-6">
            <div className="flex flex-wrap gap-3">
                <button
                    type="button"
                    onClick={() => setModalActivo('ingreso')}
                    className="rounded-lg bg-emerald-700 px-4 py-2 text-sm font-semibold text-white hover:bg-emerald-800"
                >
                    Nuevo Ingreso
                </button>
                <button
                    type="button"
                    onClick={() => setModalActivo('egreso')}
                    className="rounded-lg bg-rose-700 px-4 py-2 text-sm font-semibold text-white hover:bg-rose-800"
                >
                    Nuevo Egreso
                </button>
            </div>

            <article className="rounded-xl border border-emerald-200 bg-emerald-50 p-6">
                <p className="text-sm font-medium text-emerald-700">Saldo total de tesoreria</p>
                <p className="mt-2 text-4xl font-bold text-emerald-900">{formatCOP(totalSaldo)}</p>
                <p className="mt-1 text-sm text-emerald-700">Suma consolidada de efectivo y bancos.</p>
            </article>

            {cajasQuery.isLoading ? (
                <div className="rounded-xl border border-slate-200 bg-white p-6 text-sm text-slate-600">Cargando cajas...</div>
            ) : null}

            {cajasQuery.isError ? (
                <div className="rounded-xl border border-rose-200 bg-rose-50 p-6 text-sm text-rose-700">
                    No fue posible cargar el listado de cajas.
                </div>
            ) : null}

            {!cajasQuery.isLoading && !cajasQuery.isError ? (
                <div className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
                    {(cajasQuery.data ?? []).map((caja) => (
                        <article key={caja.id} className="rounded-xl border border-slate-200 bg-white p-5 shadow-sm">
                            <div className="flex items-start justify-between gap-3">
                                <h3 className="text-lg font-semibold text-slate-900">{caja.nombre}</h3>
                                <span className="rounded-full bg-slate-100 px-3 py-1 text-xs font-semibold text-slate-700">
                                    {getTipoCajaLabel(caja.tipoCaja)}
                                </span>
                            </div>

                            <p className="mt-4 text-sm text-slate-500">Cuenta contable</p>
                            <p className="text-sm text-slate-700">{caja.cuentaContable || 'Sin cuenta asociada'}</p>

                            <p className="mt-4 text-sm text-slate-500">Saldo actual</p>
                            <p className="text-3xl font-bold text-slate-900">{formatCOP(caja.saldoActual)}</p>
                        </article>
                    ))}
                </div>
            ) : null}

            {modalActivo ? (
                <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 p-4">
                    <div className="w-full max-w-lg rounded-xl bg-white p-6 shadow-xl">
                        <h2 className="text-lg font-semibold text-slate-900">
                            {modalActivo === 'ingreso' ? 'Registrar nuevo ingreso' : 'Registrar nuevo egreso'}
                        </h2>

                        <div className="mt-4">
                            {modalActivo === 'ingreso' ? (
                                <RegistrarIngresoForm
                                    onSuccess={() => setModalActivo(null)}
                                    onCancel={() => setModalActivo(null)}
                                />
                            ) : (
                                <RegistrarEgresoForm
                                    onSuccess={() => setModalActivo(null)}
                                    onCancel={() => setModalActivo(null)}
                                />
                            )}
                        </div>
                    </div>
                </div>
            ) : null}
        </section>
    );
}
