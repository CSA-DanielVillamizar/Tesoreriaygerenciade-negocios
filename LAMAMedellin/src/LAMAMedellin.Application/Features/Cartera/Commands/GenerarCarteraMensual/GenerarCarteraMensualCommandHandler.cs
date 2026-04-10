using LAMAMedellin.Application.Common.Exceptions;
using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.GenerarCarteraMensual;

public sealed class GenerarCarteraMensualCommandHandler(
    IMiembroRepository miembroRepository,
    ITarifaCuotaRepository tarifaCuotaRepository,
    ICuentaPorCobrarRepository cuentaPorCobrarRepository)
    : IRequestHandler<GenerarCarteraMensualCommand, int>
{
    public async Task<int> Handle(GenerarCarteraMensualCommand request, CancellationToken cancellationToken)
    {
        // TODO: Refactorizar este comando para usar ConceptoCobro como fuente de verdad
        // en lugar de TarifaCuota. Por ahora, mantiene compatibilidad legacy.

        var tarifas = await tarifaCuotaRepository.GetAllAsync(cancellationToken);
        if (tarifas.Count == 0)
        {
            throw new ExcepcionNegocio("No existen tarifas de cuota configuradas.");
        }

        var tarifasPorTipo = tarifas.ToDictionary(x => x.TipoAfiliacion, x => x.ValorMensualCOP);

        var miembrosActivos = await miembroRepository.GetActivosAsync(cancellationToken);
        var cuentasNuevas = new List<CuentaPorCobrar>();

        foreach (var miembro in miembrosActivos)
        {
            if (!tarifasPorTipo.TryGetValue(miembro.TipoAfiliacion, out var valorMensual))
            {
                throw new ExcepcionNegocio($"No existe tarifa configurada para el tipo de afiliación {miembro.TipoAfiliacion}.");
            }

            // Regla contable: si la tarifa es 0 (ej. Esposa), no se genera CxC para evitar saldos artificiales.
            if (valorMensual == 0)
            {
                continue;
            }

            // TODO: Cambiar lógica para verificar por ConceptoCobro, no por Periodo
            var existeCuenta = await cuentaPorCobrarRepository.ExistePorMiembroYPeriodoAsync(
                miembro.Id,
                request.Periodo,
                cancellationToken);

            if (existeCuenta)
            {
                continue;
            }

            // TODO: Obtener un ConceptoCobro predeterminado o configurado para este tipo de miembro
            // Por ahora, crear con placeholder (Guid.Empty será reemplazado)
            var periodo = request.Periodo;
            var inicio = ParsearFechaPeriodoInicio(periodo);
            var fin = ParsearFechaPeriodoFin(periodo);

            // Generar CxC con modelo nuevo (requiere ConceptoCobroId real)
            // NOTA: Este código fallará hasta que exista ConceptoCobro para mapping de TarifaCuota
            // Temporalmente, se mantiene la generación con valores calculados
            cuentasNuevas.Add(new CuentaPorCobrar(
                miembro.Id,
                Guid.NewGuid(), // Placeholder: será migrado a ConceptoCobro real
                inicio,
                fin,
                valorMensual));
        }

        if (cuentasNuevas.Count == 0)
        {
            return 0;
        }

        await cuentaPorCobrarRepository.AddRangeAsync(cuentasNuevas, cancellationToken);
        await cuentaPorCobrarRepository.SaveChangesAsync(cancellationToken);

        return cuentasNuevas.Count;
    }

    private static DateOnly ParsearFechaPeriodoInicio(string periodo)
    {
        var anio = int.Parse(periodo[..4]);
        var mes = int.Parse(periodo[5..]);
        return new DateOnly(anio, mes, 1);
    }

    private static DateOnly ParsearFechaPeriodoFin(string periodo)
    {
        var inicio = ParsearFechaPeriodoInicio(periodo);
        var fin = inicio.AddMonths(1).AddDays(-1);
        return fin;
    }
}
