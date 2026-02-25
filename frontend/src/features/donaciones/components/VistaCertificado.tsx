'use client';

import type { CertificadoDonacionItem } from '@/features/donaciones/hooks/useDonaciones';

type VistaCertificadoProps = {
    data: CertificadoDonacionItem;
    onClose?: () => void;
};

function formatCOP(value: number): string {
    return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        maximumFractionDigits: 0,
    }).format(value);
}

export default function VistaCertificado({ data, onClose }: VistaCertificadoProps) {
    const fecha = new Date(data.fecha);
    const fechaTexto = Number.isNaN(fecha.getTime()) ? data.fecha : fecha.toLocaleDateString('es-CO');

    return (
        <section className="mx-auto w-full max-w-3xl rounded-xl border border-slate-300 bg-white p-8 text-slate-900 shadow-lg print:shadow-none">
            <header className="border-b border-slate-300 pb-4 text-center">
                <h2 className="text-2xl font-bold uppercase">{data.fundacion.nombre}</h2>
                <p className="text-sm">NIT: {data.fundacion.nit}</p>
                <p className="text-sm">
                    {data.fundacion.direccion}, {data.fundacion.ciudad}
                </p>
                <h3 className="mt-4 text-lg font-semibold">Certificado de Donación</h3>
            </header>

            <div className="mt-6 space-y-3 text-sm leading-6">
                <p>
                    Se certifica que <strong>{data.donante.nombreDonante}</strong>, identificado con {data.donante.tipoDocumento}{' '}
                    {data.donante.numeroDocumento}, realizó una donación a favor de la Fundación.
                </p>
                <p>
                    Monto donado: <strong>{formatCOP(data.monto.valorCOP)}</strong> ({data.monto.enLetras}).
                </p>
                <p>
                    Fecha de donación: <strong>{fechaTexto}</strong>.
                </p>
                <p>
                    Código de verificación: <strong>{data.codigoVerificacion}</strong>.
                </p>

                <div className="mt-6 space-y-2 rounded-lg border border-slate-200 bg-slate-50 p-4 text-xs leading-5 text-slate-700">
                    <p>
                        La presente donación se destinará a <strong>actividades meritorias del objeto social</strong> de la Fundación.
                    </p>
                    <p>
                        <strong>La presente donación no constituye contraprestación ni retribución por servicios.</strong>
                    </p>
                    <p>
                        Esta certificación se expide para efectos tributarios en cumplimiento de los <strong>Artículos 125-1 y 125-2 del Estatuto Tributario</strong>.
                    </p>
                </div>
            </div>

            <footer className="mt-10 border-t border-slate-300 pt-6">
                <div className="mt-12 flex justify-center">
                    <div className="w-72 border-t border-slate-500 pt-2 text-center text-sm">Representante Legal / Tesorero</div>
                </div>

                <div className="flex justify-end gap-2 print:hidden">
                    {onClose ? (
                        <button type="button" onClick={onClose} className="rounded-lg border border-slate-300 px-4 py-2 text-sm">
                            Cerrar
                        </button>
                    ) : null}
                    <button type="button" onClick={() => window.print()} className="rounded-lg bg-slate-800 px-4 py-2 text-sm text-white">
                        Imprimir
                    </button>
                </div>
            </footer>
        </section>
    );
}
