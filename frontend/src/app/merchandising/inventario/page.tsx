'use client';

import { useState } from 'react';
import ModalNuevoArticulo from '@/features/merchandising/components/ModalNuevoArticulo';
import TablaArticulos from '@/features/merchandising/components/TablaArticulos';
import { useArticulos, type ArticuloItem } from '@/features/merchandising/hooks/useArticulos';

export default function InventarioPage() {
    const [modalAbierto, setModalAbierto] = useState(false);
    const [articuloEnEdicion, setArticuloEnEdicion] = useState<ArticuloItem | null>(null);
    const articulosQuery = useArticulos();

    const abrirNuevoArticulo = () => {
        setArticuloEnEdicion(null);
        setModalAbierto(true);
    };

    const abrirEdicionArticulo = (articulo: ArticuloItem) => {
        setArticuloEnEdicion(articulo);
        setModalAbierto(true);
    };

    const cerrarModalArticulo = () => {
        setModalAbierto(false);
        setArticuloEnEdicion(null);
    };

    return (
        <main className="min-h-screen bg-slate-50 px-6 py-10">
            <div className="mx-auto flex w-full max-w-6xl flex-col gap-6">
                <header className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
                    <div>
                        <h1 className="text-3xl font-bold text-slate-900">Inventario de Merchandising</h1>
                        <p className="mt-1 text-slate-600">Gestión del catálogo de artículos y stock actual</p>
                    </div>

                    <div className="flex gap-2">
                        <button
                            type="button"
                            onClick={abrirNuevoArticulo}
                            className="rounded-lg bg-slate-900 px-4 py-2 text-sm font-medium text-white"
                        >
                            Nuevo Artículo
                        </button>
                    </div>
                </header>

                <TablaArticulos
                    articulos={articulosQuery.data ?? []}
                    isLoading={articulosQuery.isLoading}
                    isError={articulosQuery.isError}
                    error={articulosQuery.error as Error | null}
                    onEditar={abrirEdicionArticulo}
                />
            </div>

            <ModalNuevoArticulo open={modalAbierto} onClose={cerrarModalArticulo} articulo={articuloEnEdicion} />
        </main>
    );
}
