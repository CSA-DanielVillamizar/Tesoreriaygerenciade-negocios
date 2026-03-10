using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Configuracion.Tarifas.Commands.ActualizarTarifasCuota;

public sealed record ActualizarTarifasCuotaCommand(
    List<ActualizarTarifaCuotaItem> Tarifas) : IRequest<List<TarifaCuotaDto>>;

public sealed record ActualizarTarifaCuotaItem(
    TipoAfiliacion TipoAfiliacion,
    decimal ValorMensualCOP);
