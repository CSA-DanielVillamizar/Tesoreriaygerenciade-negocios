'use client';

import { useState } from 'react';
import ModalNuevaVenta from '@/features/merchandising/components/ModalNuevaVenta';
import TablaVentas from '@/features/merchandising/components/TablaVentas';
import { useVentas } from '@/features/merchandising/hooks/useVentas';

export default function VentasPage() {
    const [modalVentaAbierto, setModalVentaAbierto] = useState(false);
    const ventasQuery = useVentas();

    return (
        <main className="min-h-screen bg-slate-50 px-6 py-10">
            <div className="mx-auto flex w-full max-w-6xl flex-col gap-6">
                <header className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
                    <div>
                        <h1 className="text-3xl font-bold text-slate-900">Ventas de Merchandising</h1>
                        <p className="mt-1 text-slate-600">Registro de ventas y consulta del historial</p>
                    </div>

                    <button
                        type="button"
                        onClick={() => setModalVentaAbierto(true)}
                        className="rounded-lg bg-emerald-700 px-4 py-2 text-sm font-semibold text-white"
                    >
                        Registrar Venta
                    </button>
                </header>

                <TablaVentas
                    ventas={ventasQuery.data ?? []}
                    isLoading={ventasQuery.isLoading}
                    isError={ventasQuery.isError}
                    error={ventasQuery.error as Error | null}
                />
            </div>

            <ModalNuevaVenta open={modalVentaAbierto} onClose={() => setModalVentaAbierto(false)} />
        </main>
    );
}
