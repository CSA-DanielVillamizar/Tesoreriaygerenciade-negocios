namespace Server.DTOs.Auditoria;

/// <summary>
/// DTO para el registro de auditoría del sistema (Registro Forense).
/// </summary>
public class AuditLogDto
{
    /// <summary>
    /// Id único del evento de auditoría.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora del evento.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Nombre del usuario que realizó la acción.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario (para avatar).
    /// </summary>
    public string UserEmail { get; set; } = string.Empty;

    /// <summary>
    /// Acción realizada (Crear, Editar, Eliminar, Login, Logout, etc).
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de entidad afectada (Recibo, Miembro, Usuario, etc).
    /// </summary>
    public string EntityType { get; set; } = string.Empty;

    /// <summary>
    /// ID de la entidad afectada.
    /// </summary>
    public string EntityId { get; set; } = string.Empty;

    /// <summary>
    /// Descripción amigable (ej: "Recibo #1024", "Miembro: Daniel Villamizar").
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Valores anteriores (JSON).
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// Valores nuevos (JSON).
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// Información adicional contextual.
    /// </summary>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Resultado de la operación (Success, Failed).
    /// </summary>
    public string Status { get; set; } = "Success";

    /// <summary>
    /// URL del avatar del usuario.
    /// </summary>
    public string AvatarUrl { get; set; } = string.Empty;
}
