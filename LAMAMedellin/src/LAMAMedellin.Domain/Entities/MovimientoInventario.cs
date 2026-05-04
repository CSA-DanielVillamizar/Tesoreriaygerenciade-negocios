using LAMAMedellin.Domain.Common;
using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Domain.Entities;

public sealed class MovimientoInventario : BaseEntity
{
    public Guid ProductoId { get; private set; }
    public TipoMovimientoInventario TipoMovimiento { get; private set; }
    public int Cantidad { get; private set; }
    public DateTime Fecha { get; private set; }

    public string Concepto { get; private set; } = string.Empty;
    public string? Observaciones { get; private set; }

    public Producto? Producto { get; private set; }

    private MovimientoInventario() { }

    public MovimientoInventario(
        Guid productoId,
        TipoMovimientoInventario tipoMovimiento,
        int cantidad,
        DateTime fecha,
        string concepto,
        string? observaciones = null)
    {
        if (productoId == Guid.Empty) throw new ArgumentException("ProductoId es obligatorio.", nameof(productoId));
        if (cantidad <= 0) throw new ArgumentOutOfRangeException(nameof(cantidad), "Cantidad debe ser mayor a cero.");
        if (string.IsNullOrWhiteSpace(concepto)) throw new ArgumentException("Concepto es obligatorio.", nameof(concepto));
        if (fecha.Kind != DateTimeKind.Utc) throw new ArgumentException("Fecha debe estar en UTC.", nameof(fecha));
        if (fecha > DateTime.UtcNow.AddMinutes(1)) throw new ArgumentException("Fecha no puede ser futura.", nameof(fecha));

        ProductoId = productoId;
        TipoMovimiento = tipoMovimiento;
        Cantidad = cantidad;
        Fecha = fecha;
        Concepto = concepto.Trim();
        Observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
    }
}
