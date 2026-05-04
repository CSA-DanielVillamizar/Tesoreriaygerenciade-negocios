using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Reportes.Queries.GetCarteraMora;

public sealed class GetCarteraMoraQueryHandler(ICuentaPorCobrarRepository cuentaPorCobrarRepository)
    : IRequestHandler<GetCarteraMoraQuery, CarteraMoraDto>
{
    public async Task<CarteraMoraDto> Handle(
        GetCarteraMoraQuery request,
        CancellationToken cancellationToken)
    {
        var fechaCorte = DateOnly.FromDateTime(DateTime.UtcNow);

        var cuentasEnMora = await cuentaPorCobrarRepository.GetCarteraEnMoraAsync(
            fechaCorte,
            cancellationToken);

        var detalle = cuentasEnMora
            .Select(c => new DetalleMoraDto(
                $"{c.Miembro!.Nombres} {c.Miembro.Apellidos}".Trim(),
                c.ConceptoCobro?.Nombre ?? string.Empty,
                c.FechaVencimiento,
                c.SaldoPendiente))
            .ToList();

        return new CarteraMoraDto(
            detalle.Sum(x => x.SaldoPendiente),
            detalle);
    }
}
