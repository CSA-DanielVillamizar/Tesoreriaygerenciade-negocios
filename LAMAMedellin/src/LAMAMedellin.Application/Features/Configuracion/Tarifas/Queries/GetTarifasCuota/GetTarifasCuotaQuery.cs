using MediatR;

namespace LAMAMedellin.Application.Features.Configuracion.Tarifas.Queries.GetTarifasCuota;

public sealed record GetTarifasCuotaQuery : IRequest<List<TarifaCuotaDto>>;
