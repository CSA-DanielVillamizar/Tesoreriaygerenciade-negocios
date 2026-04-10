import CrearCuentaPorCobrarForm from '@/features/cartera/components/CrearCuentaPorCobrarForm';

export const metadata = {
    title: 'Nueva Cuenta por Cobrar | L.A.M.A. Medellín',
};

export default function NuevaCuentaPorCobrarPage() {
    return (
        <main className="mx-auto max-w-lg px-4 py-10">
            <header className="mb-6">
                <h1 className="text-2xl font-bold text-slate-800">Nueva Cuenta por Cobrar</h1>
                <p className="mt-1 text-sm text-slate-500">
                    Registra una cuenta por cobrar vinculando un miembro con un concepto de cobro.
                </p>
            </header>
            <CrearCuentaPorCobrarForm />
        </main>
    );
}
