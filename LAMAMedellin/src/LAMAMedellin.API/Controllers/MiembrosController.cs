using LAMAMedellin.Application.Features.Miembros.Queries.GetMiembros;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMAMedellin.API.Controllers;

[ApiController]
[Route("api/miembros")]
[Authorize]
public sealed class MiembrosController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MiembroDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var miembros = await sender.Send(new GetMiembrosQuery(), cancellationToken);
        return Ok(miembros);
    }
}
