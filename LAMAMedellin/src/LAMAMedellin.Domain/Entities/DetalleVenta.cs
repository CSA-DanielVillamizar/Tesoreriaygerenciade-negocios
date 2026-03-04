using LAMAMedellin.Domain.Common;

namespace LAMAMedellin.Domain.Entities;

public sealed class DetalleVenta : BaseEntity
{
    public Guid VentaId { get; private set; }
    public Guid ArticuloId { get; private set; }
    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; private set; }
    public decimal CostoUnitario { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Utilidad { get; private set; }

    public Venta? Venta { get; private set; }
    public Articulo? Articulo { get; private set; }

#pragma warning disable CS8618
    private DetalleVenta() { }
#pragma warning restore CS8618

    public DetalleVenta(
        Guid ventaId,
        Guid articuloId,
        int cantidad,
        decimal precioUnitario,
        decimal costoUnitario,
        decimal subtotal,
        decimal utilidad)
    {
        if (ventaId == Guid.Empty)
        {
            throw new ArgumentException("VentaId es obligatorio.", nameof(ventaId));
        }

        if (articuloId == Guid.Empty)
        {
            throw new ArgumentException("ArticuloId es obligatorio.", nameof(articuloId));
        }

        if (cantidad <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cantidad), "Cantidad debe ser mayor a cero.");
        }

        if (precioUnitario <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(precioUnitario), "PrecioUnitario debe ser mayor a cero.");
        }

        if (costoUnitario < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(costoUnitario), "CostoUnitario no puede ser negativo.");
        }

        if (subtotal < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(subtotal), "Subtotal no puede ser negativo.");
        }

        if (utilidad < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(utilidad), "Utilidad no puede ser negativa.");
        }

        VentaId = ventaId;
        ArticuloId = articuloId;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        CostoUnitario = costoUnitario;
        Subtotal = subtotal;
        Utilidad = utilidad;
    }
}
