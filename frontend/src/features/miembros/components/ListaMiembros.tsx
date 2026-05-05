'use client';

import ModalEditarMiembro from '@/features/miembros/components/ModalEditarMiembro';
import ModalNuevoMiembro from '@/features/miembros/components/ModalNuevoMiembro';
import { useGetMiembros } from '@/features/miembros/hooks/useGetMiembros';
import type { Miembro } from '@/features/miembros/services/miembrosService';
import { useState } from 'react';

function estadoBadgeClass(esActivo: boolean): string {
    return esActivo
        ? 'border-emerald-300 bg-emerald-50 text-emerald-800'
        : 'border-slate-300 bg-slate-100 text-slate-700';
}

export default function ListaMiembros() {
    const { data, isLoading, isError, error } = useGetMiembros();
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [miembroEnEdicion, setMiembroEnEdicion] = useState<Miembro | null>(null);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);

    const abrirEdicion = (miembro: Miembro) => {
        setMiembroEnEdicion(miembro);
        setIsEditModalOpen(true);
    };

    const cerrarEdicion = () => {
        setIsEditModalOpen(false);
        setMiembroEnEdicion(null);
    };

    if (isLoading) {
        return <p className="text-sm text-slate-600">Cargando directorio de miembros...</p>;
    }

    if (isError) {
        return <p className="text-sm text-red-600">{(error as Error).message}</p>;
    }

    if (!data || data.length === 0) {
        return <p className="text-sm text-slate-600">No hay miembros registrados.</p>;
    }

    return (
        <>
            <div className="mb-6 flex justify-end">
                <button
                    type="button"
                    onClick={() => setIsModalOpen(true)}
                    className="rounded-lg bg-red-700 px-4 py-2 text-sm font-medium text-white hover:bg-red-800"
                >
                    + Nuevo Miembro
                </button>
            </div>

            <div className="grid grid-cols-1 gap-5 md:grid-cols-2 xl:grid-cols-3">
                {data.map((miembro) => (
                    <article
                        key={miembro.id}
                        className="relative rounded-2xl border border-slate-200 bg-white p-5 shadow-sm transition hover:-translate-y-0.5 hover:shadow-md"
                    >
                        <button
                            type="button"
                            onClick={() => abrirEdicion(miembro)}
                            className="absolute right-4 top-4 rounded-md border border-slate-300 bg-white px-2 py-1 text-xs font-medium text-slate-700 hover:bg-slate-50"
                        >
                            Editar
                        </button>

                        <header className="mb-4 border-b border-slate-100 pb-3">
                            <div className="flex flex-wrap items-start justify-between gap-2">
                                <div>
                                    <h3 className="text-base font-semibold text-slate-900">
                                        {miembro.nombres} {miembro.apellidos}
                                    </h3>
                                    <p className="text-sm italic text-slate-500">{miembro.apodo || 'Sin apodo'}</p>
                                </div>
                                <div className="flex flex-col items-end gap-1">
                                    <span className="inline-flex rounded-full border border-blue-200 bg-blue-50 px-2.5 py-1 text-xs font-semibold text-blue-800">
                                        {miembro.rango}
                                    </span>
                                    <span
                                        className={`inline-flex rounded-full border px-2.5 py-1 text-xs font-medium ${estadoBadgeClass(miembro.esActivo)}`}
                                    >
                                        {miembro.esActivo ? 'Activo' : 'Inactivo'}
                                    </span>
                                </div>
                            </div>
                        </header>

                        <section className="mb-3">
                            <h4 className="mb-1 text-xs font-semibold uppercase tracking-wide text-slate-500">Moto</h4>
                            <p className="text-sm text-slate-800">
                                {miembro.marcaMoto} {miembro.modeloMoto} ({miembro.cilindraje}cc) - {miembro.placa}
                            </p>
                        </section>

                        <section className="rounded-xl border border-rose-100 bg-rose-50/60 p-3">
                            <h4 className="mb-1 text-xs font-semibold uppercase tracking-wide text-slate-500">Emergencia</h4>
                            <p className="text-sm font-semibold text-red-700">Tipo de sangre: {miembro.tipoSangre}</p>
                            <p className="text-sm text-slate-700">Contacto: {miembro.nombreContactoEmergencia}</p>
                            <p className="text-sm text-slate-700">Teléfono: {miembro.telefonoContactoEmergencia}</p>
                        </section>
                    </article>
                ))}
            </div>

            <ModalNuevoMiembro isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} />
            <ModalEditarMiembro isOpen={isEditModalOpen} miembro={miembroEnEdicion} onClose={cerrarEdicion} />
        </>
    );
}
