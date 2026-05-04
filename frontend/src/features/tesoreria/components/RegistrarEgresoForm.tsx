'use client';

import { useRegistrarEgreso } from '@/features/tesoreria/hooks/useRegistrarEgreso';
import {
    registrarEgresoSchema,
    type RegistrarEgresoFormInput,
    type RegistrarEgresoFormValues,
} from '@/features/tesoreria/schemas/tesoreriaSchemas';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm } from 'react-hook-form';

type RegistrarEgresoFormProps = {
    onSuccess: () => void;
    onCancel: () => void;
};

const defaultValues: RegistrarEgresoFormInput = {
    monto: 0,
    concepto: '',
    cuentaContableId: '',
    cajaId: '',
    centroCostoId: '',
};

export default function RegistrarEgresoForm({ onSuccess, onCancel }: RegistrarEgresoFormProps) {
    const registrarEgresoMutation = useRegistrarEgreso();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<RegistrarEgresoFormInput, unknown, RegistrarEgresoFormValues>({
        resolver: zodResolver(registrarEgresoSchema),
        defaultValues,
    });

    const onSubmit = async (values: RegistrarEgresoFormValues) => {
        await registrarEgresoMutation.mutateAsync({
            monto: values.monto,
            concepto: values.concepto,
            cuentaContableId: values.cuentaContableId,
            cajaId: values.cajaId,
            centroCostoId: values.centroCostoId,
        });

        onSuccess();
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <div>
                <label className="mb-1 block text-sm font-medium text-slate-700">Monto</label>
                <input
                    type="number"
                    min={0}
                    step="0.01"
                    {...register('monto')}
                    className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                />
                {errors.monto ? <p className="mt-1 text-sm text-red-600">{errors.monto.message}</p> : null}
            </div>

            <div>
                <label className="mb-1 block text-sm font-medium text-slate-700">Concepto</label>
                <input
                    type="text"
                    {...register('concepto')}
                    className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                />
                {errors.concepto ? <p className="mt-1 text-sm text-red-600">{errors.concepto.message}</p> : null}
            </div>

            <div>
                <label className="mb-1 block text-sm font-medium text-slate-700">CuentaContableId (UUID)</label>
                <input
                    type="text"
                    {...register('cuentaContableId')}
                    className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                />
                {errors.cuentaContableId ? <p className="mt-1 text-sm text-red-600">{errors.cuentaContableId.message}</p> : null}
            </div>

            <div>
                <label className="mb-1 block text-sm font-medium text-slate-700">CajaId (UUID)</label>
                <input
                    type="text"
                    {...register('cajaId')}
                    className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                />
                {errors.cajaId ? <p className="mt-1 text-sm text-red-600">{errors.cajaId.message}</p> : null}
            </div>

            <div>
                <label className="mb-1 block text-sm font-medium text-slate-700">CentroCostoId (UUID)</label>
                <input
                    type="text"
                    {...register('centroCostoId')}
                    className="w-full rounded-lg border border-slate-300 px-3 py-2 text-slate-900"
                />
                {errors.centroCostoId ? <p className="mt-1 text-sm text-red-600">{errors.centroCostoId.message}</p> : null}
            </div>

            {registrarEgresoMutation.error ? (
                <p className="rounded-lg border border-red-200 bg-red-50 p-2 text-sm text-red-700">
                    {registrarEgresoMutation.error.message}
                </p>
            ) : null}

            <div className="flex justify-end gap-2">
                <button
                    type="button"
                    onClick={onCancel}
                    className="rounded-lg border border-slate-300 px-4 py-2 text-sm font-medium text-slate-700"
                >
                    Cancelar
                </button>
                <button
                    type="submit"
                    disabled={registrarEgresoMutation.isPending}
                    className="rounded-lg bg-rose-700 px-4 py-2 text-sm font-medium text-white disabled:opacity-60"
                >
                    {registrarEgresoMutation.isPending ? 'Registrando...' : 'Registrar egreso'}
                </button>
            </div>
        </form>
    );
}
