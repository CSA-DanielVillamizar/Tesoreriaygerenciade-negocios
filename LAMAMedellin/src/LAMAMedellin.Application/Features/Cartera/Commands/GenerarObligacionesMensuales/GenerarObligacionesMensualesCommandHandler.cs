using LAMAMedellin.Application.Common.Exceptions;
using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.GenerarObligacionesMensuales;

public sealed class GenerarObligacionesMensualesCommandHandler(
    IMiembroRepository miembroRepository,
    ICuotaAsambleaRepository cuotaAsambleaRepository,
    ICuentaPorCobrarRepository cuentaPorCobrarRepository)
    : IRequestHandler<GenerarObligacionesMensualesCommand, int>
{
    public async Task<int> Handle(GenerarObligacionesMensualesCommand request, CancellationToken cancellationToken)
    {
        var anio = int.Parse(request.Periodo.AsSpan(0, 4));

        var cuotaAsamblea = await cuotaAsambleaRepository.GetByAnioAsync(anio, cancellationToken);
        if (cuotaAsamblea is null)
        {
            throw new ExcepcionNegocio($"No existe CuotaAsamblea para el año {anio}.");
        }

        var miembrosActivos = await miembroRepository.GetActivosAsync(cancellationToken);
        var cuentasNuevas = new List<CuentaPorCobrar>();

        foreach (var miembro in miembrosActivos)
        {
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
                cuotaAsamblea.ValorMensualCOP));
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
