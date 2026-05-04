import ListaCajas from '@/features/tesoreria/components/ListaCajas';

export default function TesoreriaPage() {
    return (
        <main className="min-h-screen bg-slate-50 px-6 py-10">
            <div className="mx-auto w-full max-w-6xl">
                <header className="mb-6">
                    <h1 className="text-3xl font-bold text-slate-900">Dashboard de Tesoreria</h1>
                    <p className="mt-1 text-slate-600">Listado de cajas, tipo, cuenta contable y saldo actual.</p>
                </header>

                <ListaCajas />
            </div>
        </main>
    );
}
