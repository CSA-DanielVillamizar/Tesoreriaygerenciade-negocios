'use client';

import { useCrearProducto } from '@/features/merchandising/hooks/useCrearProducto';
import { useState } from 'react';

type ModalNuevoProductoProps = {
    abierto: boolean;
    onCerrar: () => void;
};

const UUID_REGEX =
    /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;

export default function ModalNuevoProducto({
    abierto,
    onCerrar,
}: ModalNuevoProductoProps) {
    const crearProductoMutation = useCrearProducto();
    const [values, setValues] = useState({
        nombre: '',
        codigoSKU: '',
        precioVenta: '',
        cantidadEnStock: '',
        cantidadMinima: '',
        cuentaContableIngresoId: '',
    });
    const [validationError, setValidationError] = useState<string | null>(null);

    if (!abierto) {
        return null;
    }

    const onChange = (
        field: 'nombre' | 'codigoSKU' | 'precioVenta' | 'cantidadEnStock' | 'cantidadMinima' | 'cuentaContableIngresoId',
        value: string,
    ) => {
        setValues((previous) => ({ ...previous, [field]: value }));
    };

    const onSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (
            !values.nombre.trim() ||
            !values.codigoSKU.trim() ||
            !values.precioVenta ||
            !values.cantidadEnStock ||
            !values.cantidadMinima ||
            !values.cuentaContableIngresoId.trim()
        ) {
            setValidationError('Todos los campos son obligatorios.');
            return;
        }

        const precio = Number(values.precioVenta);
        const cantidadEnStock = Number(values.cantidadEnStock);
        const cantidadMinima = Number(values.cantidadMinima);

        if (!Number.isFinite(precio) || precio <= 0) {
            setValidationError('PrecioVenta debe ser numerico y mayor a cero.');
            return;
        }

        if (!Number.isInteger(cantidadEnStock) || cantidadEnStock < 0) {
            setValidationError('CantidadEnStock debe ser un numero entero mayor o igual a cero.');
            return;
        }

        if (!Number.isInteger(cantidadMinima) || cantidadMinima < 0) {
            setValidationError('CantidadMinima debe ser un numero entero mayor o igual a cero.');
            return;
        }

        if (!UUID_REGEX.test(values.cuentaContableIngresoId.trim())) {
            setValidationError('CuentaContableIngresoId debe tener formato UUID valido.');
            return;
        }

        setValidationError(null);
        await crearProductoMutation.mutateAsync({
            nombre: values.nombre.trim(),
            codigoSKU: values.codigoSKU.trim().toUpperCase(),
            precioVenta: precio,
            cantidadEnStock,
            cantidadMinima,
            cuentaContableIngresoId: values.cuentaContableIngresoId.trim(),
        });

        setValues({
            nombre: '',
            codigoSKU: '',
            precioVenta: '',
            cantidadEnStock: '',
            cantidadMinima: '',
            cuentaContableIngresoId: '',
        });
        onCerrar();
    };

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-950/55 p-4">
            <div className="w-full max-w-xl rounded-2xl border border-slate-200 bg-white p-6 shadow-2xl">
                <div className="flex items-start justify-between gap-4">
                    <div>
                        <h2 className="text-xl font-bold text-slate-900">Nuevo producto</h2>
                        <p className="mt-1 text-sm text-slate-600">Crear item de merchandising para inventario y ventas.</p>
                    </div>

                    <button
                        type="button"
                        onClick={onCerrar}
                        className="rounded-lg border border-slate-300 px-3 py-1.5 text-sm font-medium text-slate-700"
                    >
                        Cerrar
                    </button>
                </div>

                <form className="mt-5 grid grid-cols-1 gap-4" onSubmit={onSubmit}>
                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">Nombre</label>
                        <input
                            type="text"
                            value={values.nombre}
                            onChange={(event) => onChange('nombre', event.target.value)}
                            placeholder="Parche L.A.M.A. Oficial"
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>

                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">SKU</label>
                        <input
                            type="text"
                            value={values.codigoSKU}
                            onChange={(event) => onChange('codigoSKU', event.target.value)}
                            placeholder="P-OFC-01"
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>

                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">PrecioVenta (COP)</label>
                        <input
                            type="number"
                            min="0"
                            step="0.01"
                            value={values.precioVenta}
                            onChange={(event) => onChange('precioVenta', event.target.value)}
                            placeholder="35000"
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>

                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">CantidadEnStock (inicial)</label>
                        <input
                            type="number"
                            min="0"
                            step="1"
                            value={values.cantidadEnStock}
                            onChange={(event) => onChange('cantidadEnStock', event.target.value)}
                            placeholder="25"
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>

                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">CantidadMinima</label>
                        <input
                            type="number"
                            min="0"
                            step="1"
                            value={values.cantidadMinima}
                            onChange={(event) => onChange('cantidadMinima', event.target.value)}
                            placeholder="5"
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>

                    <div>
                        <label className="mb-1 block text-sm font-medium text-slate-700">CuentaContableIngresoId</label>
                        <input
                            type="text"
                            value={values.cuentaContableIngresoId}
                            onChange={(event) => onChange('cuentaContableIngresoId', event.target.value)}
                            placeholder="00000000-0000-0000-0000-000000000000"
                            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                        />
                    </div>

                    {(validationError || crearProductoMutation.isError) ? (
                        <div className="rounded-lg border border-rose-200 bg-rose-50 px-3 py-2 text-sm text-rose-700">
                            {validationError ?? 'No fue posible crear el producto.'}
                        </div>
                    ) : null}

                    <div className="flex items-center justify-end gap-2 pt-1">
                        <button
                            type="button"
                            onClick={onCerrar}
                            className="rounded-lg border border-slate-300 px-4 py-2 text-sm font-medium text-slate-700"
                        >
                            Cancelar
                        </button>

                        <button
                            type="submit"
                            disabled={crearProductoMutation.isPending}
                            className="rounded-lg bg-indigo-700 px-4 py-2 text-sm font-medium text-white transition hover:bg-indigo-800 disabled:cursor-not-allowed disabled:opacity-60"
                        >
                            {crearProductoMutation.isPending ? 'Guardando...' : 'Crear producto'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
