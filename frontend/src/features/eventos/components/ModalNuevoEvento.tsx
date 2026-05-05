'use client';

import { useCrearEvento } from '@/features/eventos/hooks/useCrearEvento';
import type { CreateEventoPayload } from '@/features/eventos/services/eventosService';
import { useEffect } from 'react';
import { useForm } from 'react-hook-form';

type ModalNuevoEventoProps = {
    isOpen: boolean;
    onClose: () => void;
};

type NuevoEventoFormValues = {
    nombre: string;
    descripcion: string;
    fechaProgramada: string;
    lugarEncuentro: string;
    destino: string;
    tipoEvento: number;
};

const tipoEventoOptions = [
    { value: 1, label: 'Rodada' },
    { value: 2, label: 'Social' },
    { value: 3, label: 'Reunión' },
    { value: 4, label: 'Benéfico' },
    { value: 5, label: 'Otro' },
] as const;

function getDefaultFechaProgramada(): string {
    const date = new Date();
    date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
    return date.toISOString().slice(0, 16);
}

const defaultValues: NuevoEventoFormValues = {
    nombre: '',
    descripcion: '',
    fechaProgramada: getDefaultFechaProgramada(),
    lugarEncuentro: '',
    destino: '',
    tipoEvento: 1,
};

function labelClassName(): string {
    return 'mb-1 block text-sm font-medium text-slate-700';
}

function inputClassName(): string {
    return 'w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-red-100 focus:border-red-500 focus:ring-2';
}

function toUtcIsoFromLocalInput(localDateTime: string): string {
    return new Date(localDateTime).toISOString();
}

export default function ModalNuevoEvento({ isOpen, onClose }: ModalNuevoEventoProps) {
    const crearEvento = useCrearEvento();

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<NuevoEventoFormValues>({ defaultValues });

    useEffect(() => {
        if (!isOpen) {
            reset({ ...defaultValues, fechaProgramada: getDefaultFechaProgramada() });
        }
    }, [isOpen, reset]);

    if (!isOpen) {
        return null;
    }

    const onSubmit = async (values: NuevoEventoFormValues) => {
        const payload: CreateEventoPayload = {
            nombre: values.nombre.trim(),
            descripcion: values.descripcion.trim(),
            fechaProgramada: toUtcIsoFromLocalInput(values.fechaProgramada),
            lugarEncuentro: values.lugarEncuentro.trim(),
            destino: values.destino.trim() || null,
            tipoEvento: Number(values.tipoEvento),
        };

        try {
            await crearEvento.mutateAsync(payload);
            onClose();
        } catch {
            return;
        }
    };

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/55 px-4 py-6">
            <div className="max-h-[95vh] w-full max-w-2xl overflow-y-auto rounded-2xl bg-white p-6 shadow-2xl">
                <div className="mb-4 flex items-center justify-between">
                    <h2 className="text-xl font-semibold text-slate-900">Nuevo Evento</h2>
                    <button
                        type="button"
                        onClick={onClose}
                        className="rounded-md border border-slate-300 px-3 py-1.5 text-sm text-slate-700 hover:bg-slate-50"
                    >
                        Cerrar
                    </button>
                </div>

                <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">
                    <section className="rounded-xl border border-slate-200 p-4">
                        <h3 className="mb-3 text-sm font-semibold uppercase tracking-wide text-slate-600">Datos del Evento</h3>
                        <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                            <div className="md:col-span-2">
                                <label className={labelClassName()}>Nombre</label>
                                <input
                                    {...register('nombre', { required: 'Nombre es obligatorio.' })}
                                    className={inputClassName()}
                                />
                                {errors.nombre && <p className="mt-1 text-xs text-red-600">{errors.nombre.message}</p>}
                            </div>

                            <div className="md:col-span-2">
                                <label className={labelClassName()}>Descripción</label>
                                <textarea
                                    rows={3}
                                    {...register('descripcion', { required: 'Descripción es obligatoria.' })}
                                    className={inputClassName()}
                                />
                                {errors.descripcion && <p className="mt-1 text-xs text-red-600">{errors.descripcion.message}</p>}
                            </div>

                            <div>
                                <label className={labelClassName()}>Fecha y hora</label>
                                <input
                                    type="datetime-local"
                                    {...register('fechaProgramada', { required: 'Fecha programada es obligatoria.' })}
                                    className={inputClassName()}
                                />
                                {errors.fechaProgramada && <p className="mt-1 text-xs text-red-600">{errors.fechaProgramada.message}</p>}
                            </div>

                            <div>
                                <label className={labelClassName()}>Tipo</label>
                                <select {...register('tipoEvento', { valueAsNumber: true })} className={inputClassName()}>
                                    {tipoEventoOptions.map((option) => (
                                        <option key={option.value} value={option.value}>{option.label}</option>
                                    ))}
                                </select>
                            </div>

                            <div>
                                <label className={labelClassName()}>Lugar de encuentro</label>
                                <input
                                    {...register('lugarEncuentro', { required: 'Lugar de encuentro es obligatorio.' })}
                                    className={inputClassName()}
                                />
                                {errors.lugarEncuentro && <p className="mt-1 text-xs text-red-600">{errors.lugarEncuentro.message}</p>}
                            </div>

                            <div>
                                <label className={labelClassName()}>Destino</label>
                                <input {...register('destino')} className={inputClassName()} placeholder="Opcional" />
                            </div>
                        </div>
                    </section>

                    {crearEvento.error && (
                        <div className="rounded-lg border border-red-200 bg-red-50 px-3 py-2 text-sm text-red-700">
                            {crearEvento.error.message}
                        </div>
                    )}

                    <div className="flex items-center justify-end gap-3">
                        <button
                            type="button"
                            onClick={onClose}
                            className="rounded-lg border border-slate-300 px-4 py-2 text-sm font-medium text-slate-700 hover:bg-slate-50"
                        >
                            Cancelar
                        </button>
                        <button
                            type="submit"
                            disabled={crearEvento.isPending}
                            className="rounded-lg bg-red-700 px-4 py-2 text-sm font-medium text-white hover:bg-red-800 disabled:cursor-not-allowed disabled:opacity-60"
                        >
                            {crearEvento.isPending ? 'Guardando...' : 'Crear evento'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
