using LAMAMedellin.Application.Common.Exceptions;
using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.GenerarObligacionesMensuales;

public sealed class GenerarObligacionesMensualesCommandHandler(
    IMiembroRepository miembroRepository,
    ITarifaCuotaRepository tarifaCuotaRepository,
    ICuentaPorCobrarRepository cuentaPorCobrarRepository)
    : IRequestHandler<GenerarObligacionesMensualesCommand, int>
{
    public async Task<int> Handle(GenerarObligacionesMensualesCommand request, CancellationToken cancellationToken)
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
            var tipoAfiliacion = MapearTipoAfiliacionDesdeRango(miembro.Rango);

            if (!tarifasPorTipo.TryGetValue(tipoAfiliacion, out var valorMensual))
            {
                throw new ExcepcionNegocio($"No existe tarifa configurada para el rango {miembro.Rango}.");
            }

            if (valorMensual == 0)
            {
                continue;
            }

            var existeCuenta = await cuentaPorCobrarRepository.ExistePorMiembroYPeriodoAsync(
                miembro.Id,
                request.Periodo,
                cancellationToken);

            if (existeCuenta)
            {
                continue;
            }

            var periodo = request.Periodo;
            var inicio = ParsearFechaPeriodoInicio(periodo);
            var fin = ParsearFechaPeriodoFin(periodo);

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

    private static TipoAfiliacion MapearTipoAfiliacionDesdeRango(RangoClub rango)
    {
        return rango switch
        {
            RangoClub.Aspirante => TipoAfiliacion.Prospect,
            RangoClub.Prospecto => TipoAfiliacion.Prospect,
            RangoClub.MiembroActivo => TipoAfiliacion.FullColor,
            RangoClub.Directivo => TipoAfiliacion.Asociado,
            _ => TipoAfiliacion.Prospect
        };
    }
}
