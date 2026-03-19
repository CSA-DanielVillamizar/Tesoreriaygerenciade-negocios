using LAMAMedellin.Application.Common.Exceptions;
using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Application.Common.Interfaces.Services;
using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Commands.ActualizarImagenProducto;

/// <summary>
/// Handler que:
/// 1. Valida que el producto exista.
/// 2. Sube la imagen a Azure Blob Storage vía IFileStorageService.
/// 3. Actualiza la URL en el aggregate root Producto.
/// 4. Persiste el cambio.
/// </summary>
public sealed class ActualizarImagenProductoCommandHandler(
    IProductoRepository productoRepository,
    IFileStorageService fileStorageService) : IRequestHandler<ActualizarImagenProductoCommand, string>
{
    public async Task<string> Handle(
        ActualizarImagenProductoCommand request,
        CancellationToken cancellationToken)
    {
        var producto = await productoRepository.GetByIdAsync(request.ProductoId, cancellationToken)
            ?? throw new ExcepcionNegocio($"Producto con Id '{request.ProductoId}' no encontrado.");

        // Validar extensión permitida (evitar subida de archivos arbitrarios)
        var extension = Path.GetExtension(request.NombreArchivo).ToLowerInvariant();
        var extensionesPermitidas = new HashSet<string> { ".jpg", ".jpeg", ".png", ".webp" };

        if (!extensionesPermitidas.Contains(extension))
        {
            throw new ExcepcionNegocio(
                $"Extensión '{extension}' no permitida. Use: {string.Join(", ", extensionesPermitidas)}");
        }

        var imageUrl = await fileStorageService.UploadFileAsync(
            request.Imagen,
            request.NombreArchivo,
            request.ContentType,
            folderName: "productos",
            ct: cancellationToken);

        producto.ActualizarImagen(imageUrl);

        await productoRepository.SaveChangesAsync(cancellationToken);

        return imageUrl;
    }
}
