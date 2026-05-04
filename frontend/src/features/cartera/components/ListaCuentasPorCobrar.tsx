'use client';

import { useRegistrarPago } from '@/features/cartera/hooks/useRegistrarPago';
import { registrarPagoCarteraSchema } from '@/features/cartera/schemas/carteraSchemas';
import type { CuentaPorCobrarItem } from '@/features/cartera/services/carteraService';
import { useGetCajas } from '@/features/tesoreria/hooks/useGetCajas';
import { useState } from 'react';

type ListaCuentasPorCobrarProps = {
    cuentas: CuentaPorCobrarItem[];
    isLoading: boolean;
    error: Error | null;
};

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    }).format(value);
}

function formatDate(value: string): string {
    if (!value) {
        return '-';
    }

    const date = new Date(`${value}T00:00:00`);
    if (Number.isNaN(date.getTime())) {
        return value;
    }

    return new Intl.DateTimeFormat('es-CO').format(date);
}

function estadoLabel(estado: number): string {
    switch (estado) {
        case 1:
            return 'Pendiente';
        case 2:
            return 'Pagada Parcial';
        case 3:
            return 'Pagada';
        case 4:
            return 'Anulada';
        default:
            return 'Desconocido';
    }
}

export default function ListaCuentasPorCobrar({ cuentas, isLoading, error }: ListaCuentasPorCobrarProps) {
    const registrarPagoMutation = useRegistrarPago();
    const cajasQuery = useGetCajas();
    const [cuentaActiva, setCuentaActiva] = useState<CuentaPorCobrarItem | null>(null);
    const [montoPago, setMontoPago] = useState<string>('');
    const [cajaId, setCajaId] = useState<string>('');
    const [errorMonto, setErrorMonto] = useState<string | null>(null);

    const abrirPago = (cuenta: CuentaPorCobrarItem) => {
        setCuentaActiva(cuenta);
        setMontoPago(String(cuenta.saldoPendiente));
        setCajaId(cajasQuery.data?.[0]?.id ?? '');
        setErrorMonto(null);
    };

    const cerrarPago = () => {
        setCuentaActiva(null);
        setMontoPago('');
        setCajaId('');
        setErrorMonto(null);
    };

    const onSubmitPago = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (!cuentaActiva) {
            return;
        }

        const parsed = registrarPagoCarteraSchema.safeParse({ monto: montoPago, cajaId });
        if (!parsed.success) {
            setErrorMonto(parsed.error.issues[0]?.message ?? 'Monto invalido.');
            return;
        }

        setErrorMonto(null);

        await registrarPagoMutation.mutateAsync({
            cuentaPorCobrarId: cuentaActiva.id,
            monto: parsed.data.monto,
            cajaId: parsed.data.cajaId,
        });

        cerrarPago();
    };

    if (isLoading) {
        return <p className="text-sm text-slate-600">Cargando cuentas por cobrar...</p>;
    }

    if (error) {
        return (
            <div className="rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
                Error al cargar cuentas por cobrar: {error.message}
            </div>
        );
    }

    if (cuentas.length === 0) {
        return (
            <div className="rounded-lg border border-slate-200 bg-slate-50 px-4 py-6 text-center text-sm text-slate-600">
                No hay cuentas por cobrar para los filtros seleccionados.
            </div>
        );
    }

    return (
        <div className="overflow-hidden rounded-xl border border-slate-200 bg-white shadow-sm">
            <div className="overflow-x-auto">
                <table className="min-w-full divide-y divide-slate-200">
                    <thead className="bg-slate-50">
                        <tr>
                            <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Miembro</th>
                            <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Concepto</th>
                            <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Emisión</th>
                            <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Vencimiento</th>
                            <th className="px-4 py-3 text-right text-xs font-semibold uppercase tracking-wide text-slate-600">Valor Total</th>
                            <th className="px-4 py-3 text-right text-xs font-semibold uppercase tracking-wide text-slate-600">Saldo Pendiente</th>
                            <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Estado</th>
                            <th className="px-4 py-3 text-right text-xs font-semibold uppercase tracking-wide text-slate-600">Acciones</th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-slate-200 bg-white">
                        {cuentas.map((cuenta) => (
                            <tr key={cuenta.id} className="hover:bg-slate-50">
                                <td className="whitespace-nowrap px-4 py-3 text-sm text-slate-900">{cuenta.nombreCompletoMiembro}</td>
                                <td className="whitespace-nowrap px-4 py-3 text-sm text-slate-700">{cuenta.nombreConcepto}</td>
                                <td className="whitespace-nowrap px-4 py-3 text-sm text-slate-700">{formatDate(cuenta.fechaEmision)}</td>
                                <td className="whitespace-nowrap px-4 py-3 text-sm text-slate-700">{formatDate(cuenta.fechaVencimiento)}</td>
                                <td className="whitespace-nowrap px-4 py-3 text-right text-sm text-slate-900">{formatCOP(cuenta.valorTotal)}</td>
                                <td className="whitespace-nowrap px-4 py-3 text-right text-sm font-semibold text-slate-900">{formatCOP(cuenta.saldoPendiente)}</td>
                                <td className="whitespace-nowrap px-4 py-3 text-sm text-slate-700">{estadoLabel(cuenta.estado)}</td>
                                <td className="whitespace-nowrap px-4 py-3 text-right text-sm">
                                    {cuenta.estado === 1 || cuenta.estado === 2 ? (
                                        <button
                                            type="button"
                                            className="rounded-md bg-emerald-700 px-3 py-1.5 text-xs font-semibold text-white hover:bg-emerald-800"
                                            onClick={() => abrirPago(cuenta)}
                                        >
                                            Pagar
                                        </button>
                                    ) : (
                                        <span className="text-slate-400">-</span>
                                    )}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {cuentaActiva ? (
                <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 p-4">
                    <div className="w-full max-w-md rounded-xl bg-white p-5 shadow-xl">
                        <h3 className="text-lg font-semibold text-slate-900">Registrar pago</h3>
                        <p className="mt-1 text-sm text-slate-600">
                            {cuentaActiva.nombreCompletoMiembro} - saldo pendiente {formatCOP(cuentaActiva.saldoPendiente)}
                        </p>

                        <form className="mt-4 space-y-3" onSubmit={onSubmitPago}>
                            <div>
                                <label htmlFor="caja-destino" className="mb-1 block text-sm font-medium text-slate-700">
                                    Caja Destino
                                </label>
                                <select
                                    id="caja-destino"
                                    className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm text-slate-900"
                                    value={cajaId}
                                    onChange={(e) => setCajaId(e.target.value)}
                                    disabled={cajasQuery.isLoading || (cajasQuery.data?.length ?? 0) === 0}
                                >
                                    <option value="">Seleccione...</option>
                                    {(cajasQuery.data ?? []).map((caja) => (
                                        <option key={caja.id} value={caja.id}>
                                            {caja.nombre}
                                        </option>
                                    ))}
                                </select>
                            </div>

                            <div>
                                <label htmlFor="monto-pago" className="mb-1 block text-sm font-medium text-slate-700">
                                    Monto
                                </label>
                                <input
                                    id="monto-pago"
                                    type="number"
                                    step="0.01"
                                    min="0"
                                    className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm text-slate-900"
                                    value={montoPago}
                                    onChange={(e) => setMontoPago(e.target.value)}
                                />
                                {errorMonto ? <p className="mt-1 text-xs text-red-600">{errorMonto}</p> : null}
                            </div>

                            {registrarPagoMutation.error ? (
                                <p className="rounded-md border border-red-200 bg-red-50 px-3 py-2 text-sm text-red-700">
                                    {registrarPagoMutation.error.message}
                                </p>
                            ) : null}

                            <div className="flex justify-end gap-2">
                                <button
                                    type="button"
                                    onClick={cerrarPago}
                                    className="rounded-md border border-slate-300 px-3 py-2 text-sm font-medium text-slate-700"
                                >
                                    Cancelar
                                </button>
                                <button
                                    type="submit"
                                    disabled={registrarPagoMutation.isPending}
                                    className="rounded-md bg-emerald-700 px-3 py-2 text-sm font-semibold text-white disabled:opacity-60"
                                >
                                    {registrarPagoMutation.isPending ? 'Registrando...' : 'Confirmar pago'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            ) : null}
        </div>
    );
}
