'use client';

import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useActualizarMiembro } from '@/features/miembros/hooks/useActualizarMiembro';
import type { ActualizarMiembroPayload, Miembro } from '@/features/miembros/services/miembrosService';

type ModalEditarMiembroProps = {
    isOpen: boolean;
    miembro: Miembro | null;
    onClose: () => void;
};

type EditarMiembroFormValues = {
    nombres: string;
    apellidos: string;
    documentoIdentidad: string;
    apodo: string;
    fechaIngreso: string;
    rango: number;
    tipoSangre: number;
    nombreContactoEmergencia: string;
    telefonoContactoEmergencia: string;
    marcaMoto: string;
    modeloMoto: string;
    cilindraje: number;
    placa: string;
    esActivo: boolean;
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

const rangoOptions = [
    { value: 1, label: 'Aspirante' },
    { value: 2, label: 'Prospecto' },
    { value: 3, label: 'MiembroActivo' },
    { value: 4, label: 'Directivo' },
] as const;

function labelClassName(): string {
    return 'mb-1 block text-sm font-medium text-slate-700';
}

function inputClassName(): string {
    return 'w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-red-100 focus:border-red-500 focus:ring-2';
}

function mapTipoSangreToValue(tipoSangre: string): number {
    return tipoSangreOptions.find((option) => option.label === tipoSangre)?.value ?? 1;
}

function mapRangoToValue(rango: string): number {
    return rangoOptions.find((option) => option.label === rango)?.value ?? 1;
}

function getDefaultValues(miembro: Miembro | null): EditarMiembroFormValues {
    if (!miembro) {
        return {
            nombres: '',
            apellidos: '',
            documentoIdentidad: '',
            apodo: '',
            fechaIngreso: new Date().toISOString().slice(0, 10),
            rango: 1,
            tipoSangre: 1,
            nombreContactoEmergencia: '',
            telefonoContactoEmergencia: '',
            marcaMoto: '',
            modeloMoto: '',
            cilindraje: 150,
            placa: '',
            esActivo: true,
        };
    }

    return {
        nombres: miembro.nombres,
        apellidos: miembro.apellidos,
        documentoIdentidad: miembro.documentoIdentidad,
        apodo: miembro.apodo,
        fechaIngreso: miembro.fechaIngreso,
        rango: mapRangoToValue(miembro.rango),
        tipoSangre: mapTipoSangreToValue(miembro.tipoSangre),
        nombreContactoEmergencia: miembro.nombreContactoEmergencia,
        telefonoContactoEmergencia: miembro.telefonoContactoEmergencia,
        marcaMoto: miembro.marcaMoto,
        modeloMoto: miembro.modeloMoto,
        cilindraje: miembro.cilindraje,
        placa: miembro.placa,
        esActivo: miembro.esActivo,
    };
}

export default function ModalEditarMiembro({ isOpen, miembro, onClose }: ModalEditarMiembroProps) {
    const actualizarMiembro = useActualizarMiembro();

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<EditarMiembroFormValues>({
        defaultValues: getDefaultValues(miembro),
    });

    useEffect(() => {
        reset(getDefaultValues(miembro));
    }, [miembro, reset]);

    if (!isOpen || !miembro) {
        return null;
    }

    const onSubmit = async (values: EditarMiembroFormValues) => {
        const payload: ActualizarMiembroPayload = {
            tipoSangre: Number(values.tipoSangre),
            nombreContactoEmergencia: values.nombreContactoEmergencia.trim(),
            telefonoContactoEmergencia: values.telefonoContactoEmergencia.trim(),
            marcaMoto: values.marcaMoto.trim(),
            modeloMoto: values.modeloMoto.trim(),
            cilindraje: Number(values.cilindraje),
            placa: values.placa.trim().toUpperCase(),
            rango: Number(values.rango),
            esActivo: values.esActivo,
        };

        try {
            await actualizarMiembro.mutateAsync({ id: miembro.id, payload });
            onClose();
        } catch {
            return;
        }
    };

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/55 px-4 py-6">
            <div className="max-h-[95vh] w-full max-w-3xl overflow-y-auto rounded-2xl bg-white p-6 shadow-2xl">
                <div className="mb-4 flex items-center justify-between">
                    <h2 className="text-xl font-semibold text-slate-900">Editar Miembro</h2>
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
                                <input {...register('nombres')} disabled className={inputClassName()} />
                            </div>
                            <div>
                                <label className={labelClassName()}>Apellidos</label>
                                <input {...register('apellidos')} disabled className={inputClassName()} />
                            </div>
                            <div>
                                <label className={labelClassName()}>Documento</label>
                                <input {...register('documentoIdentidad')} disabled className={inputClassName()} />
                            </div>
                            <div>
                                <label className={labelClassName()}>Apodo</label>
                                <input {...register('apodo')} disabled className={inputClassName()} />
                            </div>
                            <div>
                                <label className={labelClassName()}>Fecha de Ingreso</label>
                                <input type="date" {...register('fechaIngreso')} disabled className={inputClassName()} />
                            </div>
                            <div>
                                <label className={labelClassName()}>Rango</label>
                                <select {...register('rango', { valueAsNumber: true })} className={inputClassName()}>
                                    {rangoOptions.map((option) => (
                                        <option key={option.value} value={option.value}>{option.label}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="md:col-span-2">
                                <label className="inline-flex items-center gap-2 text-sm text-slate-700">
                                    <input type="checkbox" {...register('esActivo')} className="h-4 w-4 rounded border-slate-300" />
                                    Miembro activo
                                </label>
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

                    {actualizarMiembro.error && (
                        <div className="rounded-lg border border-red-200 bg-red-50 px-3 py-2 text-sm text-red-700">
                            {actualizarMiembro.error.message}
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
                            disabled={actualizarMiembro.isPending}
                            className="rounded-lg bg-red-700 px-4 py-2 text-sm font-medium text-white hover:bg-red-800 disabled:cursor-not-allowed disabled:opacity-60"
                        >
                            {actualizarMiembro.isPending ? 'Guardando...' : 'Guardar cambios'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
