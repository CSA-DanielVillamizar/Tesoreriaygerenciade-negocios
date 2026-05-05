using LAMAMedellin.Domain.Common;
using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Domain.Entities;

public sealed class Usuario : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string EntraObjectId { get; private set; } = string.Empty;
    public RolSistema Rol { get; private set; }
    public bool EsActivo { get; private set; }
    public Guid? MiembroId { get; private set; }

#pragma warning disable CS8618
    private Usuario() { }
#pragma warning restore CS8618

    public Usuario(string email, string entraObjectId, RolSistema rol, bool esActivo, Guid? miembroId)
    {
        Email = email;
        EntraObjectId = entraObjectId;
        Rol = rol;
        EsActivo = esActivo;
        MiembroId = miembroId;
    }

    public void AsignarRol(RolSistema nuevoRol)
    {
        Rol = nuevoRol;
    }
}
