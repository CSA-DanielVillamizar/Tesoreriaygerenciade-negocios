'use client';

import { useCrearMiembro } from '@/features/cartera/hooks/useCrearMiembro';
import {
  crearMiembroSchema,
  type CrearMiembroFormInput,
  type CrearMiembroFormValues,
} from '@/features/cartera/schemas/carteraSchemas';
import { zodResolver } from '@hookform/resolvers/zod';
import { useState } from 'react';
import { useForm } from 'react-hook-form';

const defaultValues: CrearMiembroFormInput = {
  documentoIdentidad: '',
  nombres: '',
  apellidos: '',
  apodo: '',
  fechaIngreso: '',
  tipoMiembro: 2,
};

const tiposMiembro = [
  { value: 1, label: 'Prospecto' },
  { value: 2, label: 'Activo' },
  { value: 3, label: 'Rodando' },
  { value: 4, label: 'Retirado' },
];

export default function CrearMiembroForm() {
  const [mensajeExito, setMensajeExito] = useState('');
  const [mensajeError, setMensajeError] = useState('');

  const { mutateAsync, isPending } = useCrearMiembro({
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
  } = useForm<CrearMiembroFormInput, unknown, CrearMiembroFormValues>({
    resolver: zodResolver(crearMiembroSchema),
    defaultValues,
  });

  const onSubmit = async (values: CrearMiembroFormValues) => {
    await mutateAsync({
      documentoIdentidad: values.documentoIdentidad,
      nombres: values.nombres,
      apellidos: values.apellidos,
      apodo: values.apodo,
      fechaIngreso: values.fechaIngreso,
      tipoMiembro: values.tipoMiembro,
    });

    reset({
      ...defaultValues,
      tipoMiembro: values.tipoMiembro,
    });
  };

  return (
    <section className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">
        <div>
          <label className="mb-1 block text-sm font-medium text-slate-700">Documento de identidad</label>
          <input
            type="text"
            {...register('documentoIdentidad')}
            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
            placeholder="Ej: CC-12345678"
          />
          {errors.documentoIdentidad && <p className="mt-1 text-sm text-red-600">{errors.documentoIdentidad.message}</p>}
        </div>

        <div>
          <label className="mb-1 block text-sm font-medium text-slate-700">Nombres</label>
          <input
            type="text"
            {...register('nombres')}
            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
            placeholder="Ej: Daniel"
          />
          {errors.nombres && <p className="mt-1 text-sm text-red-600">{errors.nombres.message}</p>}
        </div>

        <div>
          <label className="mb-1 block text-sm font-medium text-slate-700">Apellidos</label>
          <input
            type="text"
            {...register('apellidos')}
            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
            placeholder="Ej: Villamizar"
          />
          {errors.apellidos && <p className="mt-1 text-sm text-red-600">{errors.apellidos.message}</p>}
        </div>

        <div>
          <label className="mb-1 block text-sm font-medium text-slate-700">Apodo</label>
          <input
            type="text"
            {...register('apodo')}
            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
            placeholder="Opcional"
          />
          {errors.apodo && <p className="mt-1 text-sm text-red-600">{errors.apodo.message}</p>}
        </div>

        <div>
          <label className="mb-1 block text-sm font-medium text-slate-700">Fecha de ingreso</label>
          <input
            type="date"
            {...register('fechaIngreso')}
            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
          />
          {errors.fechaIngreso && <p className="mt-1 text-sm text-red-600">{errors.fechaIngreso.message}</p>}
        </div>

        <div>
          <label className="mb-1 block text-sm font-medium text-slate-700">Tipo de miembro</label>
          <select
            {...register('tipoMiembro')}
            className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-blue-100 focus:border-blue-500 focus:ring-2"
          >
            {tiposMiembro.map((tipo) => (
              <option key={tipo.value} value={tipo.value}>
                {tipo.label}
              </option>
            ))}
          </select>
          {errors.tipoMiembro && <p className="mt-1 text-sm text-red-600">{errors.tipoMiembro.message}</p>}
        </div>

        {mensajeError && (
          <div className="rounded-lg border border-red-200 bg-red-50 px-3 py-2 text-sm text-red-700">{mensajeError}</div>
        )}

        {mensajeExito && (
          <div className="rounded-lg border border-emerald-200 bg-emerald-50 px-3 py-2 text-sm text-emerald-700">{mensajeExito}</div>
        )}

        <button
          type="submit"
          disabled={isPending}
          className="inline-flex items-center rounded-lg bg-blue-700 px-4 py-2 text-sm font-medium text-white transition hover:bg-blue-800 disabled:cursor-not-allowed disabled:opacity-60"
        >
          {isPending ? 'Guardando...' : 'Crear miembro'}
        </button>
      </form>
    </section>
  );
}
