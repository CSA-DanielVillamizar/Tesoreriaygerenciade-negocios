import CarteraMoraWidget from '@/features/reportes/components/CarteraMoraWidget';
import EstadoResultadosWidget from '@/features/reportes/components/EstadoResultadosWidget';

export default function ReportesPage() {
    return (
        <main className="min-h-screen bg-slate-50 px-6 py-10">
            <div className="mx-auto flex w-full max-w-7xl flex-col gap-6">
                <header>
                    <h1 className="text-3xl font-bold text-slate-900">Reportes Financieros</h1>
                    <p className="mt-1 text-slate-600">Vista consolidada para estado de resultados y cartera en mora.</p>
                </header>

                <section className="grid grid-cols-1 gap-6 xl:grid-cols-2">
                    <EstadoResultadosWidget />
                    <CarteraMoraWidget />
                </section>
            </div>
        </main>
    );
}
