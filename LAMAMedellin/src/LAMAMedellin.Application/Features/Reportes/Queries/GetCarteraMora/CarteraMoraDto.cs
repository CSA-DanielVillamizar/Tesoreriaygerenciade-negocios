namespace LAMAMedellin.Application.Features.Reportes.Queries.GetCarteraMora;

public sealed record CarteraMoraDto(
    decimal TotalEnMora,
    IReadOnlyList<DetalleMoraDto> DetalleMora);

public sealed record DetalleMoraDto(
    string NombreMiembro,
    string Concepto,
    DateOnly FechaVencimiento,
    decimal SaldoPendiente);
