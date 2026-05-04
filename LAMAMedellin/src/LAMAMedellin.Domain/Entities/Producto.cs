using LAMAMedellin.Domain.Common;

namespace LAMAMedellin.Domain.Entities;

public sealed class Producto : BaseEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string CodigoSKU { get; private set; } = string.Empty;
    public decimal PrecioVenta { get; private set; }
    public int CantidadEnStock { get; private set; }
    public int CantidadMinima { get; private set; }

    public Guid CuentaContableIngresoId { get; private set; }
    public CuentaContable? CuentaContableIngreso { get; private set; }

    public string? ImageUrl { get; private set; }
    public List<MovimientoInventario> Movimientos { get; private set; } = [];

    private Producto() { }

    public Producto(
        string nombre,
        string codigoSku,
        decimal precioVenta,
        int cantidadEnStock,
        int cantidadMinima,
        Guid cuentaContableIngresoId)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre es obligatorio.", nameof(nombre));
        if (string.IsNullOrWhiteSpace(codigoSku)) throw new ArgumentException("CodigoSKU es obligatorio.", nameof(codigoSku));
        if (precioVenta <= 0) throw new ArgumentOutOfRangeException(nameof(precioVenta), "PrecioVenta debe ser mayor a cero.");
        if (cantidadEnStock < 0) throw new ArgumentOutOfRangeException(nameof(cantidadEnStock), "CantidadEnStock no puede ser negativa.");
        if (cantidadMinima < 0) throw new ArgumentOutOfRangeException(nameof(cantidadMinima), "CantidadMinima no puede ser negativa.");
        if (cuentaContableIngresoId == Guid.Empty) throw new ArgumentException("CuentaContableIngresoId es obligatorio.", nameof(cuentaContableIngresoId));

        Nombre = nombre.Trim();
        CodigoSKU = codigoSku.Trim().ToUpperInvariant();
        PrecioVenta = precioVenta;
        CantidadEnStock = cantidadEnStock;
        CantidadMinima = cantidadMinima;
        CuentaContableIngresoId = cuentaContableIngresoId;
    }

    public void AjustarStock(int delta)
    {
        var nuevoStock = CantidadEnStock + delta;
        if (nuevoStock < 0)
        {
            throw new InvalidOperationException("No hay stock suficiente para la operación.");
        }

        CantidadEnStock = nuevoStock;
    }

    public bool EstaEnStockBajo() => CantidadEnStock <= CantidadMinima;

    public void ActualizarPrecio(decimal nuevoPrecio)
    {
        if (nuevoPrecio <= 0) throw new ArgumentOutOfRangeException(nameof(nuevoPrecio), "PrecioVenta debe ser mayor a cero.");
        PrecioVenta = nuevoPrecio;
    }

    public void ActualizarCantidadMinima(int cantidadMinima)
    {
        if (cantidadMinima < 0) throw new ArgumentOutOfRangeException(nameof(cantidadMinima), "CantidadMinima no puede ser negativa.");
        CantidadMinima = cantidadMinima;
    }

    public void ActualizarImagen(string? url)
    {
        ImageUrl = url;
    }
}
