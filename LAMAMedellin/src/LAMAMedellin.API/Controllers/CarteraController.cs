using LAMAMedellin.Application.Features.Cartera.Commands.GenerarObligacionesMensuales;
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
}

public sealed record GenerarObligacionesRequest(string Periodo);
