import CrearMiembroForm from '@/features/cartera/components/CrearMiembroForm';

export default function CrearMiembroPage() {
    return (
        <main className="mx-auto max-w-3xl px-6 py-10">
            <header className="mb-6">
                <h1 className="text-2xl font-semibold text-slate-900">Crear Miembro</h1>
                <p className="mt-1 text-sm text-slate-600">
                    Registra un nuevo miembro para el modulo de cartera y afiliaciones.
                </p>
            </header>

            <CrearMiembroForm />
        </main>
    );
}
