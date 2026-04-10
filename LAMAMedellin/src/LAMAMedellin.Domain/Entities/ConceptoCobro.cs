using LAMAMedellin.Domain.Common;

namespace LAMAMedellin.Domain.Entities;

public sealed class ConceptoCobro : BaseEntity
{
    public string Nombre { get; private set; }
    public decimal ValorCOP { get; private set; }
    public int PeriodicidadMensual { get; private set; }
    public Guid CuentaContableIngresoId { get; private set; }

    // Constructor privado para EF Core
#pragma warning disable CS8618
    private ConceptoCobro() { }
#pragma warning restore CS8618

    public ConceptoCobro(
        string nombre,
        decimal valorCop,
        int periodicidadMensual,
        Guid cuentaContableIngresoId)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Nombre es obligatorio.", nameof(nombre));
        }

        if (valorCop <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(valorCop), "ValorCOP debe ser mayor a cero.");
        }

        if (periodicidadMensual <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(periodicidadMensual), "PeriodicidadMensual debe ser mayor a cero.");
        }

        if (cuentaContableIngresoId == Guid.Empty)
        {
            throw new ArgumentException("CuentaContableIngresoId es obligatorio.", nameof(cuentaContableIngresoId));
        }

        Nombre = nombre.Trim();
        ValorCOP = valorCop;
        PeriodicidadMensual = periodicidadMensual;
        CuentaContableIngresoId = cuentaContableIngresoId;
    }

    public void ActualizarValor(decimal nuevoValor)
    {
        if (nuevoValor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(nuevoValor), "El nuevo valor debe ser mayor a cero.");
        }

        ValorCOP = nuevoValor;
    }
}
