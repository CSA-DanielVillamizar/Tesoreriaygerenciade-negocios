import DetalleEvento from '@/features/eventos/components/DetalleEvento';

type EventoDetallePageProps = {
    params: Promise<{ id: string }>;
};

export default async function EventoDetallePage({ params }: EventoDetallePageProps) {
    const resolvedParams = await params;

    return (
        <main className="mx-auto max-w-7xl px-6 py-10">
            <DetalleEvento eventoId={resolvedParams.id} />
        </main>
    );
}
