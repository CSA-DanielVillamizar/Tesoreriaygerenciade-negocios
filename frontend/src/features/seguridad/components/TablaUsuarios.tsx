'use client';

import { useAsignarRol } from '@/features/seguridad/hooks/useAsignarRol';
import { useGetUsuarios } from '@/features/seguridad/hooks/useGetUsuarios';
import { RolSistema, type UsuarioDto } from '@/features/seguridad/services/usuariosService';
import { useState } from 'react';

const rolOptions: Array<{ value: RolSistema; label: string }> = [
    { value: RolSistema.Administrador, label: 'Administrador' },
    { value: RolSistema.Tesorero, label: 'Tesorero' },
    { value: RolSistema.Logistica, label: 'Logistica' },
    { value: RolSistema.CapitanRuta, label: 'CapitanRuta' },
];

function estadoClass(esActivo: boolean): string {
    return esActivo
        ? 'border-emerald-300 bg-emerald-50 text-emerald-800'
        : 'border-slate-300 bg-slate-100 text-slate-700';
}

function sortByEmail(a: UsuarioDto, b: UsuarioDto): number {
    return a.email.localeCompare(b.email);
}

export default function TablaUsuarios() {
    const { data, isLoading, isError, error } = useGetUsuarios();
    const asignarRolMutation = useAsignarRol();
    const [updatingUsuarioId, setUpdatingUsuarioId] = useState<string | null>(null);

    if (isLoading) {
        return <p className="text-sm text-slate-600">Cargando usuarios del sistema...</p>;
    }

    if (isError) {
        return <p className="text-sm text-red-600">{(error as Error).message}</p>;
    }

    const usuarios = [...(data ?? [])].sort(sortByEmail);

    if (usuarios.length === 0) {
        return <p className="text-sm text-slate-600">No hay usuarios sincronizados.</p>;
    }

    const onRolChange = async (usuarioId: string, nuevoRol: RolSistema) => {
        try {
            setUpdatingUsuarioId(usuarioId);
            await asignarRolMutation.mutateAsync({
                id: usuarioId,
                payload: { nuevoRol },
            });
        } finally {
            setUpdatingUsuarioId(null);
        }
    };

    return (
        <div className="overflow-x-auto rounded-xl border border-slate-200 bg-white shadow-sm">
            <table className="min-w-full divide-y divide-slate-200">
                <thead className="bg-slate-50">
                    <tr>
                        <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Email</th>
                        <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Estado</th>
                        <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide text-slate-600">Rol</th>
                    </tr>
                </thead>
                <tbody className="divide-y divide-slate-100">
                    {usuarios.map((usuario) => {
                        const isUpdating = updatingUsuarioId === usuario.id && asignarRolMutation.isPending;

                        return (
                            <tr key={usuario.id}>
                                <td className="px-4 py-3 text-sm text-slate-800">{usuario.email}</td>
                                <td className="px-4 py-3 text-sm">
                                    <span className={`inline-flex rounded-full border px-2.5 py-1 text-xs font-medium ${estadoClass(usuario.esActivo)}`}>
                                        {usuario.esActivo ? 'Activo' : 'Inactivo'}
                                    </span>
                                </td>
                                <td className="px-4 py-3 text-sm">
                                    <select
                                        value={usuario.rol}
                                        onChange={(event) => onRolChange(usuario.id, Number(event.target.value) as RolSistema)}
                                        disabled={isUpdating}
                                        className="w-full max-w-xs rounded-lg border border-slate-300 px-3 py-2 text-slate-900 outline-none ring-red-100 focus:border-red-500 focus:ring-2 disabled:cursor-not-allowed disabled:opacity-70"
                                    >
                                        {rolOptions.map((option) => (
                                            <option key={option.value} value={option.value}>
                                                {option.label}
                                            </option>
                                        ))}
                                    </select>
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>

            {asignarRolMutation.error && (
                <div className="border-t border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
                    {asignarRolMutation.error.message}
                </div>
            )}
        </div>
    );
}
