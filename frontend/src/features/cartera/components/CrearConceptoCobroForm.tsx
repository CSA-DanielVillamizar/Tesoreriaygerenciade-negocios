'use client';

import { useCrearConceptoCobro } from '@/features/cartera/hooks/useCrearConceptoCobro';
import {
    crearConceptoCobroSchema,
    type CrearConceptoCobroFormInput,
    type CrearConceptoCobroFormValues,
} from '@/features/cartera/schemas/carteraSchemas';
import { zodResolver } from '@hookform/resolvers/zod';
import { useState } from 'react';
import { useForm } from 'react-hook-form';

const defaultValues: CrearConceptoCobroFormInput = {
    nombre: '',
    valorCOP: 0,
    periodicidadMensual: 1,
    cuentaContableIngresoId: '',
};

export default function CrearConceptoCobroForm() {
    const [mensajeExito, setMensajeExito] = useState('');
    const [mensajeError, setMensajeError] = useState('');

    const { mutateAsync, isPending } = useCrearConceptoCobro({
        onSuccessNotification: (message) => {
            setMensajeError('');
            setMensajeExito(message);
        },
        onErrorNotification: (message) => {
            setMensajeExito('');
            setMensajeError(message);
        },
    });

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<CrearConceptoCobroFormInput, unknown, CrearConceptoCobroFormValues>({
        resolver: zodResolver(crearConceptoCobroSchema),
        defaultValues,
    });

    const onSubmit = async (values: CrearConceptoCobroFormValues) => {
        await mutateAsync({
            nombre: values.nombre,
            valorCOP: values.valorCOP,
            periodicidadMensual: values.periodicidadMensual,
            cuentaContableIngresoId: values.cuentaContableIngresoId,
        });

        reset(defaultValues);
    };

    return (
        <section className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
            <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">

                {/* Nombre */}
                <div>
                    <label className="mb-1 block text-sm font-medium text-slate-700">Nombre</label>
                    <input
                        type="text"
                        {...register('nombre')}
                        className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
                        placeholder="Ej: Cuota mensual"
                    />
                    {errors.nombre && <p className="mt-1 text-sm text-red-600">{errors.nombre.message}</p>}
                </div>

                {/* Valor COP */}
                <div>
                    <label className="mb-1 block text-sm font-medium text-slate-700">Valor (COP)</label>
                    <input
                        type="number"
                        min={1}
                        step={1}
                        {...register('valorCOP')}
                        className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
                        placeholder="Ej: 50000"
                    />
                    {errors.valorCOP && <p className="mt-1 text-sm text-red-600">{errors.valorCOP.message}</p>}
                </div>

                {/* Periodicidad mensual */}
                <div>
                    <label className="mb-1 block text-sm font-medium text-slate-700">Periodicidad mensual</label>
                    <input
                        type="number"
                        min={1}
                        step={1}
                        {...register('periodicidadMensual')}
                        className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
                        placeholder="Ej: 1 (cada mes)"
                    />
                    {errors.periodicidadMensual && (
                        <p className="mt-1 text-sm text-red-600">{errors.periodicidadMensual.message}</p>
                    )}
                </div>

                {/* Cuenta contable ingreso ID */}
                <div>
                    <label className="mb-1 block text-sm font-medium text-slate-700">
                        ID Cuenta Contable de Ingreso
                    </label>
                    <input
                        type="text"
                        {...register('cuentaContableIngresoId')}
                        className="w-full rounded-lg border border-slate-300 px-3 py-2 font-mono text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
                        placeholder="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
                    />
                    <p className="mt-1 text-xs text-slate-500">
                        Ingrese el UUID de la cuenta contable. Puede consultarlo en el catálogo de cuentas.
                    </p>
                    {errors.cuentaContableIngresoId && (
                        <p className="mt-1 text-sm text-red-600">{errors.cuentaContableIngresoId.message}</p>
                    )}
                </div>

                {/* Mensajes de estado */}
                {mensajeExito && (
                    <div className="rounded-lg bg-green-50 px-4 py-3 text-sm text-green-800">
                        {mensajeExito}
                    </div>
                )}
                {mensajeError && (
                    <div className="rounded-lg bg-red-50 px-4 py-3 text-sm text-red-800">
                        {mensajeError}
                    </div>
                )}

                <button
                    type="submit"
                    disabled={isPending}
                    className="w-full rounded-lg bg-blue-600 px-4 py-2 font-semibold text-white transition hover:bg-blue-700 disabled:opacity-50"
                >
                    {isPending ? 'Guardando…' : 'Crear concepto de cobro'}
                </button>
            </form>
        </section>
    );
}
