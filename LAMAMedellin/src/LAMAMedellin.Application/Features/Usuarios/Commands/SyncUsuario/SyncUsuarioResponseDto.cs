namespace LAMAMedellin.Application.Features.Usuarios.Commands.SyncUsuario;

public sealed record SyncUsuarioResponseDto(
    Guid UsuarioId,
    string Rol);
