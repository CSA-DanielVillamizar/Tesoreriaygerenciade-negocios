using LAMAMedellin.Domain.Common;
using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Domain.Entities;

public sealed class Miembro : BaseEntity
{
    #region Propiedades Cartera (Modelo Novo)
    public string DocumentoIdentidad { get; private set; }
    public string Nombres { get; private set; }
    public string Apellidos { get; private set; }
    public string Apodo { get; private set; }
    public DateOnly FechaIngreso { get; private set; }
    public TipoMiembro TipoMiembro { get; private set; }
    #endregion

    #region Propiedades Legacy (Deprecadas - Para Refactorización Futura)
    // TODO: Refactorizar todos los módulos que usan estas propiedades
    //  - Miembros module (queries, commands)
    // - Migracion module (ImportarHistoricoCommandHandler)
    // - Cartera module (GenerarCarteraMensual, GenerarObligacionesMensuales)

    [Obsolete("Usar Nombres en su lugar. TODO: Refactorizar módulos dependientes")]
    public string Nombre { get; private set; }

    [Obsolete("Usar DocumentoIdentidad en su lugar. TODO: Refactorizar módulos dependientes")]
    public string Documento { get; private set; }

    [Obsolete("Propiedad legacy sin mapeo en modelo novo. TODO: Definir estrategia")]
    public string Email { get; private set; }

    [Obsolete("Propiedad legacy sin mapeo en modelo novo. TODO: Definir estrategia")]
    public string Telefono { get; private set; }

    [Obsolete("Usar TipoMiembro en su lugar. TODO: Refactorizar módulos dependientes")]
    public TipoAfiliacion TipoAfiliacion { get; private set; }

    [Obsolete("Propiedad legacy sin mapeo directo. TODO: Refactorizar módulos dependientes")]
    public EstadoMiembro Estado { get; private set; }
    #endregion

    // Constructor privado para EF Core
#pragma warning disable CS8618
    private Miembro() { }
#pragma warning restore CS8618

    public Miembro(
        string documentoIdentidad,
        string nombres,
        string apellidos,
        string apodo,
        DateOnly fechaIngreso,
        TipoMiembro tipoMiembro)
    {
        DocumentoIdentidad = ValidarTextoRequerido(documentoIdentidad, nameof(documentoIdentidad));
        Nombres = ValidarTextoRequerido(nombres, nameof(nombres));
        Apellidos = ValidarTextoRequerido(apellidos, nameof(apellidos));
        Apodo = (apodo ?? string.Empty).Trim();

        if (fechaIngreso == default)
        {
            throw new ArgumentException("FechaIngreso es obligatoria.", nameof(fechaIngreso));
        }

        FechaIngreso = fechaIngreso;
        TipoMiembro = tipoMiembro;

        // Compatibilidad temporal para módulos dependientes
#pragma warning disable CS0618
        Nombre = Nombres;
        Documento = DocumentoIdentidad;
        Email = "sin-correo@lama.local";
        Telefono = "N/A";
        TipoAfiliacion = TipoAfiliacion.Prospect;
        Estado = MapearEstadoDesdeTipoMiembro(tipoMiembro);
#pragma warning restore CS0618
    }

    public Miembro(
        string nombre,
        string apellidos,
        string documento,
        string email,
        string telefono,
        TipoAfiliacion tipoAfiliacion,
        EstadoMiembro estado)
    {
#pragma warning disable CS0618
        Nombre = ValidarTextoRequerido(nombre, nameof(nombre));
        Nombres = Nombre;

        Apellidos = ValidarTextoRequerido(apellidos, nameof(apellidos));

        Documento = ValidarTextoRequerido(documento, nameof(documento));
        DocumentoIdentidad = Documento;

        Apodo = string.Empty;
        FechaIngreso = DateOnly.FromDateTime(DateTime.UtcNow);

        Email = ValidarTextoRequerido(email, nameof(email));
        Telefono = ValidarTextoRequerido(telefono, nameof(telefono));
        TipoAfiliacion = tipoAfiliacion;
        Estado = estado;
        TipoMiembro = MapearTipoMiembroDesdeEstado(estado);
#pragma warning restore CS0618
    }

#pragma warning disable CS0618
    public void ActualizarDatos(
        string nombre,
        string apellidos,
        string documento,
        string email,
        string telefono,
        TipoAfiliacion tipoAfiliacion,
        EstadoMiembro estado)
    {
        Nombre = ValidarTextoRequerido(nombre, nameof(nombre));
        Nombres = Nombre;

        Apellidos = ValidarTextoRequerido(apellidos, nameof(apellidos));

        Documento = ValidarTextoRequerido(documento, nameof(documento));
        DocumentoIdentidad = Documento;

        Email = ValidarTextoRequerido(email, nameof(email));
        Telefono = ValidarTextoRequerido(telefono, nameof(telefono));
        TipoAfiliacion = tipoAfiliacion;
        Estado = estado;
        TipoMiembro = MapearTipoMiembroDesdeEstado(estado);
    }

    public void Desactivar()
    {
        Estado = EstadoMiembro.Inactivo;
        TipoMiembro = TipoMiembro.Retirado;
    }
#pragma warning restore CS0618

    private static string ValidarTextoRequerido(string valor, string nombreParametro)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ArgumentException($"{nombreParametro} es obligatorio.", nombreParametro);
        }

        return valor.Trim();
    }

    private static EstadoMiembro MapearEstadoDesdeTipoMiembro(TipoMiembro tipoMiembro)
    {
        return tipoMiembro switch
        {
            TipoMiembro.Activo => EstadoMiembro.Activo,
            TipoMiembro.Rodando => EstadoMiembro.Suspendido,
            TipoMiembro.Retirado => EstadoMiembro.Inactivo,
            _ => EstadoMiembro.Inactivo
        };
    }

    private static TipoMiembro MapearTipoMiembroDesdeEstado(EstadoMiembro estado)
    {
        return estado switch
        {
            EstadoMiembro.Activo => TipoMiembro.Activo,
            EstadoMiembro.Suspendido => TipoMiembro.Rodando,
            EstadoMiembro.Inactivo => TipoMiembro.Retirado,
            _ => TipoMiembro.Prospecto
        };
    }
}
