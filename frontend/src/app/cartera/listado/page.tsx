import TablaCartera from '@/features/cartera/components/TablaCartera';
import Link from 'next/link';

export default function ListadoCarteraPage() {
    return (
        <main className="mx-auto max-w-7xl px-6 py-10">
            <header className="mb-6 flex items-center justify-between">
                <div>
                    <h1 className="text-2xl font-semibold text-slate-900">Cartera Pendiente</h1>
                    <p className="mt-1 text-sm text-slate-600">
                        Consulta de todas las cuentas por cobrar con saldo pendiente de pago.
                    </p>
                </div>
                <Link
                    href="/cartera/generar"
                    className="inline-flex items-center rounded-md bg-slate-700 px-4 py-2 text-sm font-medium text-white shadow-sm transition-colors hover:bg-slate-800 focus:outline-none focus:ring-2 focus:ring-slate-500 focus:ring-offset-2"
                >
                    Generar Cuotas
                </Link>
            </header>

            <TablaCartera />
        </main>
    );
}
