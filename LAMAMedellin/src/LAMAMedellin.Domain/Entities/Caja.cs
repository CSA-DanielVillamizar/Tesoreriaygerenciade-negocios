using LAMAMedellin.Domain.Common;
using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Domain.Entities;

public sealed class Caja : BaseEntity
{
    public string Nombre { get; private set; }
    public TipoCaja TipoCaja { get; private set; }
    public decimal SaldoActual { get; private set; }
    public Guid CuentaContableId { get; private set; }

    public CuentaContable? CuentaContable { get; private set; }

#pragma warning disable CS8618
    private Caja() { }
#pragma warning restore CS8618

    public Caja(string nombre, TipoCaja tipoCaja, decimal saldoActual, Guid cuentaContableId)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Nombre es obligatorio.", nameof(nombre));
        }

        if (saldoActual < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(saldoActual), "SaldoActual no puede ser negativo.");
        }

        if (cuentaContableId == Guid.Empty)
        {
            throw new ArgumentException("CuentaContableId es obligatorio.", nameof(cuentaContableId));
        }

        Nombre = nombre.Trim();
        TipoCaja = tipoCaja;
        SaldoActual = saldoActual;
        CuentaContableId = cuentaContableId;
    }

    public void AplicarIngreso(decimal monto)
    {
        if (monto <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(monto), "El monto debe ser mayor a cero.");
        }

        SaldoActual += monto;
    }

    public void AplicarEgreso(decimal monto)
    {
        if (monto <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(monto), "El monto debe ser mayor a cero.");
        }

        if (SaldoActual < monto)
        {
            throw new ReglaNegocioException("Saldo insuficiente en caja para registrar el egreso.");
        }

        SaldoActual -= monto;
    }
}
