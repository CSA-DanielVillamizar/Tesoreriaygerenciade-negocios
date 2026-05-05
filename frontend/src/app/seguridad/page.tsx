import TablaUsuarios from '@/features/seguridad/components/TablaUsuarios';

export default function SeguridadPage() {
    return (
        <main className="mx-auto max-w-7xl px-6 py-10">
            <header className="mb-6">
                <h1 className="text-2xl font-semibold text-slate-900">Gestión de Accesos</h1>
                <p className="mt-1 text-sm text-slate-600">
                    Administración de roles y permisos por usuario sincronizado desde Entra ID.
                </p>
            </header>

            <TablaUsuarios />
        </main>
    );
}
