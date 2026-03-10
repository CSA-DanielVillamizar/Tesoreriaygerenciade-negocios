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

            var existeCuenta = await cuentaPorCobrarRepository.ExistePorMiembroYPeriodoAsync(
                miembro.Id,
                request.Periodo,
                cancellationToken);

            if (existeCuenta)
            {
                continue;
            }

            cuentasNuevas.Add(new CuentaPorCobrar(
                miembro.Id,
                request.Periodo,
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
}
