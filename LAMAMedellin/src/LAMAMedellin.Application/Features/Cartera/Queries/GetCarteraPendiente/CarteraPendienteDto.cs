namespace LAMAMedellin.Application.Features.Cartera.Queries.GetCarteraPendiente;

public sealed record CarteraPendienteDto(
    Guid Id,
    Guid MiembroId,
    string NombreMiembro,
    DateOnly FechaEmision,
    decimal ValorTotal,
    decimal SaldoPendiente);
