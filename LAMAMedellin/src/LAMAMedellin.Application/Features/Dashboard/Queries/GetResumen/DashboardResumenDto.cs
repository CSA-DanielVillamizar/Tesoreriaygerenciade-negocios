namespace LAMAMedellin.Application.Features.Dashboard.Queries.GetResumen;

public sealed record DashboardResumenDto(
    int TotalMiembrosActivos,
    decimal TotalDineroCajas,
    string? ProximoEventoNombre,
    DateTime? ProximaFechaEvento);
