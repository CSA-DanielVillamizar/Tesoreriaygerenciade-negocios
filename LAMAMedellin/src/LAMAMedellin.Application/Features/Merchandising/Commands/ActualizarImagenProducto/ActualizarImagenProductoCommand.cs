using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Commands.ActualizarImagenProducto;

/// <summary>
/// Comando para subir o reemplazar la imagen de un producto.
/// Transporta el stream del archivo, sin depender de ASP.NET Core.
/// El Controller es responsable de extraer el Stream desde IFormFile.
/// </summary>
public sealed record ActualizarImagenProductoCommand(
    Guid ProductoId,
    Stream Imagen,
    string NombreArchivo,
    string ContentType) : IRequest<string>;
