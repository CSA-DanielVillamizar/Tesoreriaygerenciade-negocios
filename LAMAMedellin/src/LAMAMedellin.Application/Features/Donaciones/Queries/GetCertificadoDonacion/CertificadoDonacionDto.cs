namespace LAMAMedellin.Application.Features.Donaciones.Queries.GetCertificadoDonacion;

public sealed record CertificadoDonacionDto(
    Guid DonacionId,
    DateTime Fecha,
    decimal MontoCOP,
    string MontoEnLetras,
    string CodigoVerificacion,
    Guid DonanteId,
    string NombreDonante,
    string TipoDocumento,
    string NumeroDocumento,
    string Email);
