using LAMAMedellin.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.API.Controllers;

[ApiController]
[Route("api/test-db")]
public sealed class TestConnectionController(LamaDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            var totalUsuarios = await dbContext.Usuarios.CountAsync(cancellationToken);

            return Ok(new
            {
                resultado = "OK",
                totalUsuarios
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                resultado = "ERROR",
                mensaje = ex.Message
            });
        }
    }
}
