using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using MediatR;

namespace LAMAMedellin.Application.Features.Configuracion.Tarifas.Commands.ActualizarTarifasCuota;

public sealed class ActualizarTarifasCuotaCommandHandler(ITarifaCuotaRepository tarifaCuotaRepository)
    : IRequestHandler<ActualizarTarifasCuotaCommand, List<TarifaCuotaDto>>
{
    public async Task<List<TarifaCuotaDto>> Handle(ActualizarTarifasCuotaCommand request, CancellationToken cancellationToken)
    {
        var existentes = await tarifaCuotaRepository.GetAllAsync(cancellationToken);
        var mapaExistentes = existentes.ToDictionary(x => x.TipoAfiliacion);

        var nuevas = new List<TarifaCuota>();

        foreach (var tarifa in request.Tarifas)
        {
            if (mapaExistentes.TryGetValue(tarifa.TipoAfiliacion, out var actual))
            {
                actual.ActualizarValorMensual(tarifa.ValorMensualCOP);
                continue;
            }

            nuevas.Add(new TarifaCuota(tarifa.TipoAfiliacion, tarifa.ValorMensualCOP));
        }

        if (nuevas.Count > 0)
        {
            await tarifaCuotaRepository.AddRangeAsync(nuevas, cancellationToken);
        }

        await tarifaCuotaRepository.SaveChangesAsync(cancellationToken);

        var resultado = await tarifaCuotaRepository.GetAllAsync(cancellationToken);

        return resultado
            .Select(x => new TarifaCuotaDto(x.TipoAfiliacion, x.ValorMensualCOP))
            .ToList();
    }
}
