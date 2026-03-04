using LAMAMedellin.Domain.Common;

namespace LAMAMedellin.Domain.Entities;

public sealed class Beneficiario : BaseEntity
{
    public Guid ProyectoSocialId { get; private set; }
    public string NombreCompleto { get; private set; }
    public string TipoDocumento { get; private set; }
    public string NumeroDocumento { get; private set; }
    public string Email { get; private set; }
    public string Telefono { get; private set; }
    public bool TieneConsentimientoHabeasData { get; private set; }

    public ProyectoSocial? ProyectoSocial { get; private set; }

#pragma warning disable CS8618
    private Beneficiario() { }
#pragma warning restore CS8618

    public Beneficiario(
        Guid proyectoSocialId,
        string nombreCompleto,
        string tipoDocumento,
        string numeroDocumento,
        string email,
        string telefono,
        bool tieneConsentimientoHabeasData)
    {
        if (proyectoSocialId == Guid.Empty)
        {
            throw new ArgumentException("ProyectoSocialId es obligatorio.", nameof(proyectoSocialId));
        }

        ProyectoSocialId = proyectoSocialId;
        NombreCompleto = ValidarRequerido(nombreCompleto, nameof(nombreCompleto), 200);
        TipoDocumento = ValidarRequerido(tipoDocumento, nameof(tipoDocumento), 30);
        NumeroDocumento = ValidarRequerido(numeroDocumento, nameof(numeroDocumento), 30);
        Email = ValidarRequerido(email, nameof(email), 200);
        Telefono = ValidarRequerido(telefono, nameof(telefono), 30);
        TieneConsentimientoHabeasData = tieneConsentimientoHabeasData;
    }

    public void Actualizar(
        Guid proyectoSocialId,
        string nombreCompleto,
        string tipoDocumento,
        string numeroDocumento,
        string email,
        string telefono,
        bool tieneConsentimientoHabeasData)
    {
        if (proyectoSocialId == Guid.Empty)
        {
            throw new ArgumentException("ProyectoSocialId es obligatorio.", nameof(proyectoSocialId));
        }

        ProyectoSocialId = proyectoSocialId;
        NombreCompleto = ValidarRequerido(nombreCompleto, nameof(nombreCompleto), 200);
        TipoDocumento = ValidarRequerido(tipoDocumento, nameof(tipoDocumento), 30);
        NumeroDocumento = ValidarRequerido(numeroDocumento, nameof(numeroDocumento), 30);
        Email = ValidarRequerido(email, nameof(email), 200);
        Telefono = ValidarRequerido(telefono, nameof(telefono), 30);
        TieneConsentimientoHabeasData = tieneConsentimientoHabeasData;
    }

    private static string ValidarRequerido(string value, string paramName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Campo obligatorio.", paramName);
        }

        var trimmed = value.Trim();
        if (trimmed.Length > maxLength)
        {
            throw new ArgumentException($"Máximo {maxLength} caracteres.", paramName);
        }

        return trimmed;
    }
}
