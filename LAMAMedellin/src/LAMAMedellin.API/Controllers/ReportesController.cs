using LAMAMedellin.Application.Features.Reportes.Queries.GetCarteraMora;
using LAMAMedellin.Application.Features.Reportes.Queries.GetEstadoResultados;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMAMedellin.API.Controllers;

[ApiController]
[Route("api/reportes")]
[Authorize]
public sealed class ReportesController(ISender sender) : ControllerBase
{
    [HttpGet("estado-resultados")]
    [ProducesResponseType(typeof(EstadoResultadosDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEstadoResultados(
        [FromQuery] DateTime fechaInicio,
        [FromQuery] DateTime fechaFin,
        CancellationToken cancellationToken)
    {
        var reporte = await sender.Send(
            new GetEstadoResultadosQuery(fechaInicio, fechaFin),
            cancellationToken);

        return Ok(reporte);
    }

    [HttpGet("cartera-mora")]
    [ProducesResponseType(typeof(CarteraMoraDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCarteraMora(CancellationToken cancellationToken)
    {
        var reporte = await sender.Send(new GetCarteraMoraQuery(), cancellationToken);
        return Ok(reporte);
    }
}
