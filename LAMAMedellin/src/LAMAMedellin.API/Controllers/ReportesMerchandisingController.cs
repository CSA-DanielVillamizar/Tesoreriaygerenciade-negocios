using LAMAMedellin.Application.Features.Merchandising.Queries.GetResumenVentasUtilidad;
using LAMAMedellin.Application.Features.Merchandising.Queries.GetValorizacionInventario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMAMedellin.API.Controllers;

[ApiController]
[Route("api/merchandising/reportes")]
[Authorize]
public sealed class ReportesMerchandisingController(ISender sender) : ControllerBase
{
    [HttpGet("valorizacion-inventario")]
    [ProducesResponseType(typeof(ValorizacionInventarioDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetValorizacionInventario(CancellationToken cancellationToken)
    {
        var reporte = await sender.Send(new GetValorizacionInventarioQuery(), cancellationToken);
        return Ok(reporte);
    }

    [HttpGet("resumen-ventas-utilidad")]
    [ProducesResponseType(typeof(ResumenVentasUtilidadDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetResumenVentasUtilidad(
        [FromQuery] DateTime? fechaInicio,
        [FromQuery] DateTime? fechaFin,
        CancellationToken cancellationToken)
    {
        var reporte = await sender.Send(new GetResumenVentasUtilidadQuery(fechaInicio, fechaFin), cancellationToken);
        return Ok(reporte);
    }
}
