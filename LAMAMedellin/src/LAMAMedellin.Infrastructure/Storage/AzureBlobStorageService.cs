using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LAMAMedellin.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace LAMAMedellin.Infrastructure.Storage;

/// <summary>
/// Implementación del servicio de almacenamiento usando Azure Blob Storage.
/// Usa la cadena de conexión "BlobStorage" de la configuración.
/// En producción, se extrae desde Azure Key Vault vía Managed Identity.
/// </summary>
public sealed class AzureBlobStorageService : IFileStorageService
{
    private const string ContainerName = "merchandising-imagenes";

    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BlobStorage")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'BlobStorage' no está configurada. " +
                "Verifique que esté definida en appsettings o en Azure Key Vault.");

        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    /// <inheritdoc/>
    public async Task<string> UploadFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        string folderName,
        CancellationToken ct = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

        // El contenedor se crea si no existe (idempotente). Acceso público para lectura de blobs.
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: ct);

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        var blobName = $"{folderName}/{Guid.NewGuid()}{extension}";

        var blobClient = containerClient.GetBlobClient(blobName);

        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType,
                CacheControl = "public, max-age=31536000"
            }
        };

        await blobClient.UploadAsync(fileStream, uploadOptions, ct);

        return blobClient.Uri.ToString();
    }
}
