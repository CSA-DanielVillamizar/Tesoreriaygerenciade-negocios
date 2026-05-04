'use client';

import ModalEntradaInventario from '@/features/merchandising/components/ModalEntradaInventario';
import ModalNuevoProducto from '@/features/merchandising/components/ModalNuevoProducto';
import ModalVenta from '@/features/merchandising/components/ModalVenta';
import { useGetProductos } from '@/features/merchandising/hooks/useGetProductos';
import { useMemo, useState } from 'react';

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    }).format(value);
}

export default function TablaProductos() {
    const { data, isLoading, isError } = useGetProductos();
    const [nuevoProductoAbierto, setNuevoProductoAbierto] = useState(false);
    const [productoEntradaId, setProductoEntradaId] = useState<string | null>(null);
    const [productoVentaId, setProductoVentaId] = useState<string | null>(null);

    const productos = useMemo(() => data ?? [], [data]);

    return (
        <main className="min-h-screen bg-slate-50 px-6 py-10">
            <div className="mx-auto w-full max-w-6xl">
                <header className="mb-6 flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
                    <div>
                        <h1 className="text-3xl font-bold text-slate-900">Catalogo de Productos</h1>
                        <p className="mt-1 text-slate-600">Gestiona stock y registra entradas de inventario por producto.</p>
                    </div>

                    <button
                        type="button"
                        onClick={() => setNuevoProductoAbierto(true)}
                        className="rounded-lg bg-indigo-700 px-4 py-2 text-sm font-semibold text-white transition hover:bg-indigo-800"
                    >
                        + Nuevo Producto
                    </button>
                </header>

                <section className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm md:p-6">
                    <div className="overflow-x-auto">
                        <table className="min-w-full divide-y divide-slate-200 text-sm">
                            <thead>
                                <tr className="text-left text-slate-500">
                                    <th className="px-3 py-3 font-semibold">Nombre</th>
                                    <th className="px-3 py-3 font-semibold">SKU</th>
                                    <th className="px-3 py-3 font-semibold">Precio</th>
                                    <th className="px-3 py-3 font-semibold">Stock</th>
                                    <th className="px-3 py-3 font-semibold">Estado</th>
                                    <th className="px-3 py-3 font-semibold">Acciones</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-slate-100">
                                {productos.map((producto) => {
                                    const stockBajo = producto.cantidadEnStock <= producto.cantidadMinima;

                                    return (
                                        <tr key={producto.id}>
                                            <td className="px-3 py-4 text-slate-900">{producto.nombre}</td>
                                            <td className="px-3 py-4 font-medium text-slate-700">{producto.codigoSKU}</td>
                                            <td className="px-3 py-4 font-semibold text-slate-900">{formatCOP(producto.precioVenta)}</td>
                                            <td className="px-3 py-4 text-slate-700">{producto.cantidadEnStock}</td>
                                            <td className="px-3 py-4">
                                                {stockBajo ? (
                                                    <span className="inline-flex rounded-full bg-rose-100 px-2.5 py-1 text-xs font-semibold text-rose-700">
                                                        Stock Bajo
                                                    </span>
                                                ) : (
                                                    <span className="inline-flex rounded-full bg-emerald-100 px-2.5 py-1 text-xs font-semibold text-emerald-700">
                                                        OK
                                                    </span>
                                                )}
                                            </td>
                                            <td className="px-3 py-4">
                                                <div className="flex flex-wrap gap-2">
                                                    <button
                                                        type="button"
                                                        onClick={() => setProductoEntradaId(producto.id)}
                                                        className="rounded-lg bg-emerald-700 px-3 py-2 text-xs font-semibold text-white transition hover:bg-emerald-800"
                                                    >
                                                        + Entrada
                                                    </button>

                                                    <button
                                                        type="button"
                                                        onClick={() => setProductoVentaId(producto.id)}
                                                        className="rounded-lg bg-rose-700 px-3 py-2 text-xs font-semibold text-white transition hover:bg-rose-800"
                                                    >
                                                        Vender
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                    );
                                })}
                            </tbody>
                        </table>
                    </div>

                    {isLoading ? (
                        <p className="px-3 py-4 text-sm text-slate-500">Cargando productos...</p>
                    ) : null}

                    {isError ? (
                        <div className="mx-3 my-4 rounded-lg border border-rose-200 bg-rose-50 px-3 py-2 text-sm text-rose-700">
                            No fue posible cargar el catalogo de productos.
                        </div>
                    ) : null}
                </section>
            </div>

            <ModalEntradaInventario
                productoId={productoEntradaId}
                onCerrar={() => setProductoEntradaId(null)}
            />

            <ModalVenta
                productoId={productoVentaId}
                onCerrar={() => setProductoVentaId(null)}
            />

            <ModalNuevoProducto
                abierto={nuevoProductoAbierto}
                onCerrar={() => setNuevoProductoAbierto(false)}
            />
        </main>
    );
}
