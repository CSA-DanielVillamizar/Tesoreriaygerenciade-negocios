using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Queries.GetResumenVentasUtilidad;

public sealed record GetResumenVentasUtilidadQuery(DateTime? FechaInicio, DateTime? FechaFin) : IRequest<ResumenVentasUtilidadDto>;

public sealed record ResumenVentasUtilidadDto(
    decimal TotalVendido,
    decimal CostoTotalVendido,
    decimal UtilidadNeta,
    int CantidadVentas);

public sealed class GetResumenVentasUtilidadQueryHandler(IVentaRepository ventaRepository)
    : IRequestHandler<GetResumenVentasUtilidadQuery, ResumenVentasUtilidadDto>
{
    public async Task<ResumenVentasUtilidadDto> Handle(GetResumenVentasUtilidadQuery request, CancellationToken cancellationToken)
    {
        var ventas = await ventaRepository.GetAllAsync(cancellationToken);

        var fechaInicio = request.FechaInicio?.Date;
        var fechaFin = request.FechaFin?.Date;

        var ventasFiltradas = ventas.Where(venta =>
        {
            var fechaVenta = venta.Fecha.Date;
            var cumpleInicio = !fechaInicio.HasValue || fechaVenta >= fechaInicio.Value;
            var cumpleFin = !fechaFin.HasValue || fechaVenta <= fechaFin.Value;
            return cumpleInicio && cumpleFin;
        }).ToList();

        var totalVendido = ventasFiltradas.Sum(x => x.Total);
        var costoTotalVendido = ventasFiltradas.Sum(x => x.DetallesVenta.Sum(d => d.CostoUnitario * d.Cantidad));
        var utilidadNeta = totalVendido - costoTotalVendido;

        return new ResumenVentasUtilidadDto(
            totalVendido,
            costoTotalVendido,
            utilidadNeta,
            ventasFiltradas.Count);
    }
}
