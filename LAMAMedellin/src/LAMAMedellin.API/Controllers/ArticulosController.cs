using LAMAMedellin.Application.Features.Merchandising.Commands.CreateArticulo;
using LAMAMedellin.Application.Features.Merchandising.Commands.UpdateArticulo;
using LAMAMedellin.Application.Features.Merchandising.Queries.GetArticuloById;
using LAMAMedellin.Application.Features.Merchandising.Queries.GetArticulos;
using LAMAMedellin.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMAMedellin.API.Controllers;

[ApiController]
[Route("api/articulos")]
[Authorize]
public sealed class ArticulosController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LAMAMedellin.Application.Features.Merchandising.Queries.GetArticulos.ArticuloDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var articulos = await sender.Send(new GetArticulosQuery(), cancellationToken);
        return Ok(articulos);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LAMAMedellin.Application.Features.Merchandising.Queries.GetArticuloById.ArticuloDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var articulo = await sender.Send(new GetArticuloByIdQuery(id), cancellationToken);
        if (articulo is null)
        {
            return NotFound();
        }

        return Ok(articulo);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] UpsertArticuloRequest request, CancellationToken cancellationToken)
    {
        var id = await sender.Send(
            new CreateArticuloCommand(
                request.Nombre,
                request.SKU,
                request.Descripcion,
                request.Categoria,
                request.PrecioVenta,
                request.CostoPromedio,
                request.StockActual,
                request.CuentaContableIngresoId),
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpsertArticuloRequest request, CancellationToken cancellationToken)
    {
        await sender.Send(
            new UpdateArticuloCommand(
                id,
                request.Nombre,
                request.SKU,
                request.Descripcion,
                request.Categoria,
                request.PrecioVenta,
                request.CostoPromedio,
                request.StockActual,
                request.CuentaContableIngresoId),
            cancellationToken);

        return NoContent();
    }

    public sealed record UpsertArticuloRequest(
        string Nombre,
        string SKU,
        string Descripcion,
        CategoriaArticulo Categoria,
        decimal PrecioVenta,
        decimal CostoPromedio,
        int StockActual,
        Guid CuentaContableIngresoId);
}
