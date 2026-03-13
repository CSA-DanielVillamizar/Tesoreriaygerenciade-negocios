using LAMAMedellin.Domain.Common;

namespace LAMAMedellin.Domain.Entities;

public sealed class Egreso : BaseEntity
{
    public DateTime Fecha { get; private set; }
    public decimal Monto { get; private set; }
    public string Concepto { get; private set; }
    public Guid? TerceroId { get; private set; }
    public Guid CuentaContableId { get; private set; }
    public Guid CajaId { get; private set; }
    public Guid CentroCostoId { get; private set; }
    public Guid? ComprobanteContableId { get; private set; }

    public Caja? Caja { get; private set; }
    public CentroCosto? CentroCosto { get; private set; }
    public CuentaContable? CuentaContable { get; private set; }
    public Comprobante? ComprobanteContable { get; private set; }

#pragma warning disable CS8618
    private Egreso() { }
#pragma warning restore CS8618

    public Egreso(
        DateTime fecha,
        decimal monto,
        string concepto,
        Guid? terceroId,
        Guid cuentaContableId,
        Guid cajaId,
        Guid centroCostoId)
    {
        if (string.IsNullOrWhiteSpace(concepto))
        {
            throw new ArgumentException("Concepto es obligatorio.", nameof(concepto));
        }

        if (monto <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(monto), "Monto debe ser mayor a cero.");
        }

        if (cajaId == Guid.Empty)
        {
            throw new ArgumentException("CajaId es obligatorio.", nameof(cajaId));
        }

        if (cuentaContableId == Guid.Empty)
        {
            throw new ArgumentException("CuentaContableId es obligatorio.", nameof(cuentaContableId));
        }

        if (centroCostoId == Guid.Empty)
        {
            throw new ArgumentException("CentroCostoId es obligatorio.", nameof(centroCostoId));
        }

        Fecha = fecha;
        Monto = monto;
        Concepto = concepto.Trim();
        TerceroId = terceroId;
        CuentaContableId = cuentaContableId;
        CajaId = cajaId;
        CentroCostoId = centroCostoId;
    }

    public void AsignarComprobanteContable(Guid comprobanteContableId)
    {
        if (comprobanteContableId == Guid.Empty)
        {
            throw new ArgumentException("ComprobanteContableId es obligatorio.", nameof(comprobanteContableId));
        }

        ComprobanteContableId = comprobanteContableId;
    }
}
