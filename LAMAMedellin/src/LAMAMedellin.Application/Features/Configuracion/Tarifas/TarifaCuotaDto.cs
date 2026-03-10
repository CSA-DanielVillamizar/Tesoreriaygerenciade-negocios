using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Application.Features.Configuracion.Tarifas;

public sealed record TarifaCuotaDto(
    TipoAfiliacion TipoAfiliacion,
    decimal ValorMensualCOP);
