namespace LAMAMedellin.Application.Features.Merchandising.Queries.GetProductos;

public sealed record ProductoDto(
    Guid Id,
    string Nombre,
    string CodigoSKU,
    decimal PrecioVenta,
    int CantidadEnStock,
    int CantidadMinima,
    Guid CuentaContableIngresoId,
    string CuentaContableIngresoCodigo,
    string CuentaContableIngresoDescripcion,
    string? ImageUrl);
