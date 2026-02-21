using LAMAMedellin.Application.Features.Cartera.Commands.GenerarObligacionesMensuales;
using LAMAMedellin.Application.Features.Cartera.Queries.GetCarteraPendiente;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMAMedellin.API.Controllers;

[ApiController]
[Route("api/cartera")]
[Authorize]
public sealed class CarteraController(ISender sender) : ControllerBase
{
    [HttpPost("generar-obligaciones")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerarObligaciones(
        [FromBody] GenerarObligacionesRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await sender.Send(
            new GenerarObligacionesMensualesCommand(request.Periodo),
            cancellationToken);

        return Ok(new
        {
            mensaje = $"Se han generado {resultado} obligaciones para el periodo {request.Periodo}"
        });
    }

    [HttpGet("pendiente")]
    [ProducesResponseType(typeof(List<CarteraPendienteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCarteraPendiente(CancellationToken cancellationToken)
    {
        var cartera = await sender.Send(new GetCarteraPendienteQuery(), cancellationToken);
        return Ok(cartera);
    }
}

public sealed record GenerarObligacionesRequest(string Periodo);
