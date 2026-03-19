namespace LAMAMedellin.Application.Common.Interfaces.Services;

/// <summary>
/// Contrato para el servicio de almacenamiento de archivos en la nube.
/// La implementación concreta reside en Infrastructure (Azure Blob Storage).
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Sube un archivo al contenedor especificado y retorna la URL pública del blob.
    /// </summary>
    /// <param name="fileStream">Stream del archivo a subir</param>
    /// <param name="fileName">Nombre original del archivo (usado para la extensión)</param>
    /// <param name="contentType">MIME type del archivo</param>
    /// <param name="folderName">Carpeta/prefijo lógico dentro del contenedor</param>
    /// <param name="ct">Token de cancelación</param>
    /// <returns>URL pública del blob recién creado</returns>
    Task<string> UploadFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        string folderName,
        CancellationToken ct = default);
}
