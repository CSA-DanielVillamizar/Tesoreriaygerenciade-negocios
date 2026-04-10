import CrearConceptoCobroForm from '@/features/cartera/components/CrearConceptoCobroForm';

export const metadata = {
    title: 'Nuevo Concepto de Cobro | L.A.M.A. Medellín',
};

export default function NuevoConceptoCobroPage() {
    return (
        <main className="mx-auto max-w-lg px-4 py-10">
            <header className="mb-6">
                <h1 className="text-2xl font-bold text-slate-800">Nuevo Concepto de Cobro</h1>
                <p className="mt-1 text-sm text-slate-500">
                    Define un nuevo concepto recurrente para la cartera del capítulo.
                </p>
            </header>
            <CrearConceptoCobroForm />
        </main>
    );
}
