'use client';

import Image from 'next/image';
import ModalEntradaInventario, { type EntradaInventarioFormValues } from '@/features/merchandising/components/ModalEntradaInventario';
import ModalNuevoProducto, {
    type CatalogItem,
    type NuevoProductoFormValues,
} from '@/features/merchandising/components/ModalNuevoProducto';
import ModalVenta, { type VentaFormValues } from '@/features/merchandising/components/ModalVenta';
import {
    crearProducto,
    getProductos,
    registrarEntrada,
    registrarVenta,
    subirImagenProducto,
    type CrearProductoPayload,
    type ProductoMerchandising,
    type RegistrarEntradaPayload,
    type RegistrarVentaPayload,
} from '@/features/merchandising/services/merchandisingService';
import apiClient from '@/lib/apiClient';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import axios from 'axios';
import { useMemo, useState } from 'react';

type CuentaContableApiDto = {
    id?: string;
    Id?: string;
    codigo?: string;
    Codigo?: string;
    descripcion?: string;
    Descripcion?: string;
    naturaleza?: number;
    Naturaleza?: number;
    permiteMovimiento?: boolean;
    PermiteMovimiento?: boolean;
};

type CajaApiDto = {
    id?: string;
    Id?: string;
    nombre?: string;
    Nombre?: string;
};

type CentroCostoApiDto = {
    id?: string;
    Id?: string;
    nombre?: string;
    Nombre?: string;
};

type ProblemDetails = {
    title?: string;
    detail?: string;
    errors?: Record<string, string[]>;
};

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    }).format(value);
}

function getErrorMessage(error: unknown): string {
    if (axios.isAxiosError<ProblemDetails>(error)) {
        const validationErrors = error.response?.data?.errors;
        const firstValidationError = validationErrors
            ? Object.values(validationErrors).flat().find((message) => message)
            : undefined;

        return firstValidationError ?? error.response?.data?.detail ?? error.response?.data?.title ?? 'No fue posible completar la operacion.';
    }

    if (error instanceof Error && error.message) {
        return error.message;
    }

    return 'No fue posible completar la operacion.';
}

function toCrearPayload(values: NuevoProductoFormValues): CrearProductoPayload {
    return {
        nombre: values.nombre.trim(),
        sku: values.sku.trim().toUpperCase(),
        precioVentaCOP: Number(values.precioVentaCOP),
        cuentaContableIngresoId: values.cuentaContableIngresoId,
    };
}

function toEntradaPayload(values: EntradaInventarioFormValues): RegistrarEntradaPayload {
    return {
        cantidad: Number(values.cantidad),
        fecha: new Date(values.fecha).toISOString(),
        observaciones: values.observaciones.trim() || null,
    };
}

function toVentaPayload(values: VentaFormValues): RegistrarVentaPayload {
    return {
        cantidad: Number(values.cantidad),
        cajaId: values.cajaId,
        centroCostoId: values.centroCostoId,
        fecha: new Date(values.fecha).toISOString(),
        observaciones: values.observaciones.trim() || null,
    };
}

export default function MerchandisingDashboard() {
    const queryClient = useQueryClient();

    const [modalNuevoAbierto, setModalNuevoAbierto] = useState(false);
    const [productoEntradaId, setProductoEntradaId] = useState<string | null>(null);
    const [productoVentaId, setProductoVentaId] = useState<string | null>(null);
    const [mensajeExito, setMensajeExito] = useState<string | null>(null);
    // id del producto cuya imagen está siendo procesada (para spinner por fila)
    const [uploadingImageId, setUploadingImageId] = useState<string | null>(null);

    const productosQuery = useQuery({
        queryKey: ['merchandising', 'productos'],
        queryFn: getProductos,
    });

    const cuentasIngresoQuery = useQuery({
        queryKey: ['merchandising', 'catalogos', 'cuentas-ingreso'],
        queryFn: async () => {
            const response = await apiClient.get<CuentaContableApiDto[]>('/api/cuentas-contables');

            return (response.data ?? [])
                .map((item) => ({
                    id: String(item?.id ?? item?.Id ?? ''),
                    codigo: String(item?.codigo ?? item?.Codigo ?? ''),
                    descripcion: String(item?.descripcion ?? item?.Descripcion ?? ''),
                    naturaleza: Number(item?.naturaleza ?? item?.Naturaleza ?? 0),
                    permiteMovimiento: Boolean(item?.permiteMovimiento ?? item?.PermiteMovimiento ?? false),
                }))
                .filter((cuenta) => cuenta.permiteMovimiento && cuenta.naturaleza === 2)
                .map((cuenta) => ({
                    id: cuenta.id,
                    nombre: `${cuenta.codigo} - ${cuenta.descripcion}`,
                })) as CatalogItem[];
        },
    });

    const cajasQuery = useQuery({
        queryKey: ['merchandising', 'catalogos', 'cajas'],
        queryFn: async () => {
            const response = await apiClient.get<CajaApiDto[]>('/api/tesoreria/cajas');

            return (response.data ?? []).map((item) => ({
                id: String(item?.id ?? item?.Id ?? ''),
                nombre: String(item?.nombre ?? item?.Nombre ?? ''),
            })) as CatalogItem[];
        },
    });

    const centrosCostoQuery = useQuery({
        queryKey: ['merchandising', 'catalogos', 'centros-costo'],
        queryFn: async () => {
            const response = await apiClient.get<CentroCostoApiDto[]>('/api/transacciones/centros-costo');

            return (response.data ?? []).map((item) => ({
                id: String(item?.id ?? item?.Id ?? ''),
                nombre: String(item?.nombre ?? item?.Nombre ?? ''),
            })) as CatalogItem[];
        },
    });

    const crearProductoMutation = useMutation({
        mutationFn: crearProducto,
        onSuccess: async () => {
            setMensajeExito('Producto creado correctamente.');
            setModalNuevoAbierto(false);
            await queryClient.invalidateQueries({ queryKey: ['merchandising', 'productos'] });
        },
    });

    const registrarEntradaMutation = useMutation({
        mutationFn: (params: { productoId: string; payload: RegistrarEntradaPayload }) =>
            registrarEntrada(params.productoId, params.payload),
        onSuccess: async () => {
            setMensajeExito('Entrada de inventario registrada correctamente.');
            setProductoEntradaId(null);
            await queryClient.invalidateQueries({ queryKey: ['merchandising', 'productos'] });
        },
    });

    const registrarVentaMutation = useMutation({
        mutationFn: (params: { productoId: string; payload: RegistrarVentaPayload }) =>
            registrarVenta(params.productoId, params.payload),
        onSuccess: async () => {
            setMensajeExito('Venta registrada correctamente.');
            setProductoVentaId(null);
            await queryClient.invalidateQueries({ queryKey: ['merchandising', 'productos'] });
        },
    });

    const subirImagenMutation = useMutation({
        mutationFn: (params: { productoId: string; file: File }) =>
            subirImagenProducto(params.productoId, params.file),
        onSuccess: async () => {
            setMensajeExito('Imagen actualizada correctamente.');
            setUploadingImageId(null);
            await queryClient.invalidateQueries({ queryKey: ['merchandising', 'productos'] });
        },
        onError: () => {
            setUploadingImageId(null);
        },
    });

    const productoEntrada = useMemo<ProductoMerchandising | null>(() => {
        return (productosQuery.data ?? []).find((item) => item.id === productoEntradaId) ?? null;
    }, [productoEntradaId, productosQuery.data]);

    const productoVenta = useMemo<ProductoMerchandising | null>(() => {
        return (productosQuery.data ?? []).find((item) => item.id === productoVentaId) ?? null;
    }, [productoVentaId, productosQuery.data]);

    const onSubmitNuevoProducto = async (values: NuevoProductoFormValues) => {
        await crearProductoMutation.mutateAsync(toCrearPayload(values));
    };

    const onSubmitEntrada = async (values: EntradaInventarioFormValues) => {
        if (!productoEntradaId) {
            return;
        }

        await registrarEntradaMutation.mutateAsync({
            productoId: productoEntradaId,
            payload: toEntradaPayload(values),
        });
    };

    const onSubmitVenta = async (values: VentaFormValues) => {
        if (!productoVentaId) {
            return;
        }

        await registrarVentaMutation.mutateAsync({
            productoId: productoVentaId,
            payload: toVentaPayload(values),
        });
    };

    const onSeleccionarImagen = (productoId: string, file: File | undefined) => {
        if (!file) {
            return;
        }

        setMensajeExito(null);
        setUploadingImageId(productoId);
        subirImagenMutation.mutate({ productoId, file });
    };

    return (
        <main className="min-h-screen bg-slate-50 px-6 py-10">
            <div className="mx-auto w-full max-w-6xl">
                <header className="mb-6 flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
                    <div>
                        <h1 className="text-3xl font-bold text-slate-900">Merchandising</h1>
                        <p className="mt-1 text-slate-600">Catalogo de productos, entradas de inventario y ventas.</p>
                    </div>

                    <button
                        type="button"
                        onClick={() => {
                            setMensajeExito(null);
                            setModalNuevoAbierto(true);
                        }}
                        className="rounded-2xl bg-indigo-700 px-5 py-3 text-sm font-semibold text-white transition hover:bg-indigo-800"
                    >
                        + Nuevo Producto
                    </button>
                </header>

                {mensajeExito ? (
                    <div className="mb-6 rounded-xl border border-emerald-200 bg-emerald-50 px-4 py-3 text-sm text-emerald-700">
                        {mensajeExito}
                    </div>
                ) : null}

                <section className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm md:p-6">
                    <div className="overflow-x-auto">
                        <table className="min-w-full divide-y divide-slate-200 text-sm">
                            <thead>
                                <tr className="text-left text-slate-500">
                                    <th className="px-3 py-3 font-semibold">Foto</th>
                                    <th className="px-3 py-3 font-semibold">Nombre</th>
                                    <th className="px-3 py-3 font-semibold">SKU</th>
                                    <th className="px-3 py-3 font-semibold">Precio</th>
                                    <th className="px-3 py-3 font-semibold">Stock actual</th>
                                    <th className="px-3 py-3 font-semibold">Acciones</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-slate-100">
                                {(productosQuery.data ?? []).map((producto) => (
                                    <tr key={producto.id} className="align-top">
                                        {/* Columna thumbnail */}
                                        <td className="px-3 py-4">
                                            {producto.imageUrl ? (
                                                <Image
                                                    src={producto.imageUrl}
                                                    alt={producto.nombre}
                                                    width={56}
                                                    height={56}
                                                    className="h-14 w-14 rounded-lg object-cover shadow-sm"
                                                />
                                            ) : (
                                                <div className="flex h-14 w-14 items-center justify-center rounded-lg border-2 border-dashed border-slate-300 bg-slate-50 text-slate-400">
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="h-6 w-6">
                                                        <path fillRule="evenodd" d="M1.5 6a2.25 2.25 0 0 1 2.25-2.25h16.5A2.25 2.25 0 0 1 22.5 6v12a2.25 2.25 0 0 1-2.25 2.25H3.75A2.25 2.25 0 0 1 1.5 18V6ZM3 16.06V18c0 .414.336.75.75.75h16.5A.75.75 0 0 0 21 18v-1.94l-2.69-2.689a1.5 1.5 0 0 0-2.12 0l-.88.879.83.83a.75.75 0 1 1-1.06 1.06l-5-5a1.5 1.5 0 0 0-2.12 0L3 16.061Zm10.125-7.81a1.125 1.125 0 1 1 2.25 0 1.125 1.125 0 0 1-2.25 0Z" clipRule="evenodd" />
                                                    </svg>
                                                </div>
                                            )}
                                        </td>
                                        <td className="px-3 py-4 text-slate-900">
                                            <p className="font-semibold">{producto.nombre}</p>
                                            <p className="mt-1 text-xs text-slate-500">
                                                Cuenta ingreso: {producto.cuentaContableIngresoCodigo} - {producto.cuentaContableIngresoDescripcion}
                                            </p>
                                        </td>
                                        <td className="px-3 py-4 font-medium text-slate-700">{producto.sku}</td>
                                        <td className="px-3 py-4 font-semibold text-slate-900">{formatCOP(producto.precioVentaCOP)}</td>
                                        <td className="px-3 py-4">
                                            <span
                                                className={`inline-flex rounded-full px-2.5 py-1 text-xs font-semibold ${producto.cantidadStock === 0
                                                        ? 'bg-rose-100 text-rose-700'
                                                        : 'bg-emerald-100 text-emerald-700'
                                                    }`}
                                            >
                                                {producto.cantidadStock === 0 ? 'Sin stock' : `${producto.cantidadStock} unidades`}
                                            </span>
                                        </td>
                                        <td className="px-3 py-4">
                                            <div className="flex flex-wrap gap-2">
                                                {/* Botón de cámara — dispara input file oculto */}
                                                <label
                                                    htmlFor={`img-upload-${producto.id}`}
                                                    className="cursor-pointer rounded-lg bg-slate-700 px-3 py-2 text-xs font-semibold text-white transition hover:bg-slate-800 disabled:opacity-60"
                                                    title="Subir foto del producto"
                                                >
                                                    {uploadingImageId === producto.id ? '⏳' : '📷'}
                                                </label>
                                                <input
                                                    id={`img-upload-${producto.id}`}
                                                    type="file"
                                                    accept="image/jpeg,image/png,image/webp"
                                                    className="sr-only"
                                                    disabled={uploadingImageId === producto.id}
                                                    onChange={(e) => onSeleccionarImagen(producto.id, e.target.files?.[0])}
                                                />

                                                <button
                                                    type="button"
                                                    onClick={() => {
                                                        setMensajeExito(null);
                                                        setProductoEntradaId(producto.id);
                                                    }}
                                                    className="rounded-lg bg-emerald-700 px-3 py-2 text-xs font-semibold text-white transition hover:bg-emerald-800"
                                                >
                                                    Anadir Stock
                                                </button>

                                                <button
                                                    type="button"
                                                    onClick={() => {
                                                        setMensajeExito(null);
                                                        setProductoVentaId(producto.id);
                                                    }}
                                                    className="rounded-lg bg-rose-700 px-3 py-2 text-xs font-semibold text-white transition hover:bg-rose-800"
                                                >
                                                    Vender
                                                </button>
                                            </div>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>

                    {productosQuery.isLoading ? (
                        <p className="px-3 py-4 text-sm text-slate-500">Cargando catalogo...</p>
                    ) : null}

                    {productosQuery.isError ? (
                        <div className="mx-3 my-4 rounded-lg border border-rose-200 bg-rose-50 px-3 py-2 text-sm text-rose-700">
                            {getErrorMessage(productosQuery.error)}
                        </div>
                    ) : null}
                </section>
            </div>

            <ModalNuevoProducto
                abierto={modalNuevoAbierto}
                cuentasIngreso={cuentasIngresoQuery.data ?? []}
                enviando={crearProductoMutation.isPending}
                error={crearProductoMutation.isError ? getErrorMessage(crearProductoMutation.error) : null}
                onCerrar={() => setModalNuevoAbierto(false)}
                onEnviar={onSubmitNuevoProducto}
            />

            <ModalEntradaInventario
                abierto={Boolean(productoEntradaId)}
                productoNombre={productoEntrada?.nombre ?? ''}
                enviando={registrarEntradaMutation.isPending}
                error={registrarEntradaMutation.isError ? getErrorMessage(registrarEntradaMutation.error) : null}
                onCerrar={() => setProductoEntradaId(null)}
                onEnviar={onSubmitEntrada}
            />

            <ModalVenta
                abierto={Boolean(productoVentaId)}
                productoNombre={productoVenta?.nombre ?? ''}
                cajas={cajasQuery.data ?? []}
                centrosCosto={centrosCostoQuery.data ?? []}
                enviando={registrarVentaMutation.isPending}
                error={registrarVentaMutation.isError ? getErrorMessage(registrarVentaMutation.error) : null}
                onCerrar={() => setProductoVentaId(null)}
                onEnviar={onSubmitVenta}
            />
        </main>
    );
}
