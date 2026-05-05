import ListaEventos from '@/features/eventos/components/ListaEventos';

export default function EventosPage() {
    return (
        <main className="mx-auto max-w-7xl px-6 py-10">
            <header className="mb-6">
                <h1 className="text-2xl font-semibold text-slate-900">Agenda de Eventos y Rodadas</h1>
                <p className="mt-1 text-sm text-slate-600">
                    Gestión de salidas, encuentros y actividades del capítulo L.A.M.A. Medellín.
                </p>
            </header>

            <ListaEventos />
        </main>
    );
}
