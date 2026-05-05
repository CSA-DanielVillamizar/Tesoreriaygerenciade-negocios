'use client';

import { useCrearMiembro } from '@/features/miembros/hooks/useCrearMiembro';
import type { CrearMiembroPayload } from '@/features/miembros/services/miembrosService';
import { useEffect } from 'react';
import { useForm } from 'react-hook-form';

type ModalNuevoMiembroProps = {
    isOpen: boolean;
    onClose: () => void;
};

type NuevoMiembroFormValues = {
    nombres: string;
    apellidos: string;
    documentoIdentidad: string;
    apodo: string;
    fechaIngreso: string;
    tipoSangre: number;
    nombreContactoEmergencia: string;
    telefonoContactoEmergencia: string;
    marcaMoto: string;
    modeloMoto: string;
    cilindraje: number;
    placa: string;
};

const tipoSangreOptions = [
    { value: 1, label: 'O+' },
    { value: 2, label: 'O-' },
    { value: 3, label: 'A+' },
    { value: 4, label: 'A-' },
    { value: 5, label: 'B+' },
    { value: 6, label: 'B-' },
    { value: 7, label: 'AB+' },
    { value: 8, label: 'AB-' },
] as const;

const defaultValues: NuevoMiembroFormValues = {
    nombres: '',
    apellidos: '',
    documentoIdentidad: '',
    apodo: '',
    fechaIngreso: new Date().toISOString().slice(0, 10),
    tipoSangre: 1,
    nombreContactoEmergencia: '',
    telefonoContactoEmergencia: '',
    marcaMoto: '',
    modeloMoto: '',
    cilindraje: 150,
    placa: '',
};

function labelClassName(): string {
    return 'mb-1 block text-sm font-medium text-slate-700';
}

function inputClassName(): string {
    return 'w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-red-100 focus:border-red-500 focus:ring-2';
}

export default function ModalNuevoMiembro({ isOpen, onClose }: ModalNuevoMiembroProps) {
    const crearMiembro = useCrearMiembro();

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<NuevoMiembroFormValues>({ defaultValues });

    useEffect(() => {
        if (!isOpen) {
            reset(defaultValues);
        }
    }, [isOpen, reset]);

    if (!isOpen) {
        return null;
    }

    const onSubmit = async (values: NuevoMiembroFormValues) => {
        const payload: CrearMiembroPayload = {
            documentoIdentidad: values.documentoIdentidad.trim(),
            nombres: values.nombres.trim(),
            apellidos: values.apellidos.trim(),
            apodo: values.apodo.trim(),
            fechaIngreso: values.fechaIngreso,
            tipoSangre: Number(values.tipoSangre),
            nombreContactoEmergencia: values.nombreContactoEmergencia.trim(),
            telefonoContactoEmergencia: values.telefonoContactoEmergencia.trim(),
            marcaMoto: values.marcaMoto.trim(),
            modeloMoto: values.modeloMoto.trim(),
            cilindraje: Number(values.cilindraje),
            placa: values.placa.trim().toUpperCase(),
            rango: 1,
            esActivo: true,
        };

        try {
            await crearMiembro.mutateAsync(payload);
            onClose();
        } catch {
            return;
        }
    };

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/55 px-4 py-6">
            <div className="max-h-[95vh] w-full max-w-3xl overflow-y-auto rounded-2xl bg-white p-6 shadow-2xl">
                <div className="mb-4 flex items-center justify-between">
                    <h2 className="text-xl font-semibold text-slate-900">Nuevo Miembro</h2>
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
                        <h3 className="mb-3 text-sm font-semibold uppercase tracking-wide text-slate-600">Datos Personales</h3>
                        <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                            <div>
                                <label className={labelClassName()}>Nombres</label>
                                <input {...register('nombres', { required: 'Nombres es obligatorio.' })} className={inputClassName()} />
                                {errors.nombres && <p className="mt-1 text-xs text-red-600">{errors.nombres.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Apellidos</label>
                                <input {...register('apellidos', { required: 'Apellidos es obligatorio.' })} className={inputClassName()} />
                                {errors.apellidos && <p className="mt-1 text-xs text-red-600">{errors.apellidos.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Documento</label>
                                <input {...register('documentoIdentidad', { required: 'Documento es obligatorio.' })} className={inputClassName()} />
                                {errors.documentoIdentidad && <p className="mt-1 text-xs text-red-600">{errors.documentoIdentidad.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Apodo</label>
                                <input {...register('apodo', { required: 'Apodo es obligatorio.' })} className={inputClassName()} />
                                {errors.apodo && <p className="mt-1 text-xs text-red-600">{errors.apodo.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Fecha de Ingreso</label>
                                <input type="date" {...register('fechaIngreso', { required: 'Fecha de ingreso es obligatoria.' })} className={inputClassName()} />
                                {errors.fechaIngreso && <p className="mt-1 text-xs text-red-600">{errors.fechaIngreso.message}</p>}
                            </div>
                        </div>
                    </section>

                    <section className="rounded-xl border border-rose-200 bg-rose-50/40 p-4">
                        <h3 className="mb-3 text-sm font-semibold uppercase tracking-wide text-slate-600">Emergencia</h3>
                        <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                            <div>
                                <label className={labelClassName()}>Tipo de Sangre</label>
                                <select {...register('tipoSangre', { valueAsNumber: true })} className={inputClassName()}>
                                    {tipoSangreOptions.map((option) => (
                                        <option key={option.value} value={option.value}>{option.label}</option>
                                    ))}
                                </select>
                            </div>
                            <div>
                                <label className={labelClassName()}>Contacto</label>
                                <input
                                    {...register('nombreContactoEmergencia', { required: 'Contacto es obligatorio.' })}
                                    className={inputClassName()}
                                />
                                {errors.nombreContactoEmergencia && <p className="mt-1 text-xs text-red-600">{errors.nombreContactoEmergencia.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Teléfono</label>
                                <input
                                    {...register('telefonoContactoEmergencia', { required: 'Teléfono es obligatorio.' })}
                                    className={inputClassName()}
                                />
                                {errors.telefonoContactoEmergencia && <p className="mt-1 text-xs text-red-600">{errors.telefonoContactoEmergencia.message}</p>}
                            </div>
                        </div>
                    </section>

                    <section className="rounded-xl border border-blue-200 bg-blue-50/40 p-4">
                        <h3 className="mb-3 text-sm font-semibold uppercase tracking-wide text-slate-600">Motocicleta</h3>
                        <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                            <div>
                                <label className={labelClassName()}>Marca</label>
                                <input {...register('marcaMoto', { required: 'Marca es obligatoria.' })} className={inputClassName()} />
                                {errors.marcaMoto && <p className="mt-1 text-xs text-red-600">{errors.marcaMoto.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Modelo</label>
                                <input {...register('modeloMoto', { required: 'Modelo es obligatorio.' })} className={inputClassName()} />
                                {errors.modeloMoto && <p className="mt-1 text-xs text-red-600">{errors.modeloMoto.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Cilindraje</label>
                                <input
                                    type="number"
                                    {...register('cilindraje', {
                                        valueAsNumber: true,
                                        min: { value: 1, message: 'Cilindraje debe ser mayor a cero.' },
                                    })}
                                    className={inputClassName()}
                                />
                                {errors.cilindraje && <p className="mt-1 text-xs text-red-600">{errors.cilindraje.message}</p>}
                            </div>
                            <div>
                                <label className={labelClassName()}>Placa</label>
                                <input {...register('placa', { required: 'Placa es obligatoria.' })} className={inputClassName()} />
                                {errors.placa && <p className="mt-1 text-xs text-red-600">{errors.placa.message}</p>}
                            </div>
                        </div>
                    </section>

                    {crearMiembro.error && (
                        <div className="rounded-lg border border-red-200 bg-red-50 px-3 py-2 text-sm text-red-700">
                            {crearMiembro.error.message}
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
                            disabled={crearMiembro.isPending}
                            className="rounded-lg bg-red-700 px-4 py-2 text-sm font-medium text-white hover:bg-red-800 disabled:cursor-not-allowed disabled:opacity-60"
                        >
                            {crearMiembro.isPending ? 'Guardando...' : 'Crear miembro'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
