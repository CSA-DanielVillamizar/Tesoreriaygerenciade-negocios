namespace LAMAMedellin.Application.Features.Usuarios.Queries.GetUsuarios;

public sealed record UsuarioDto(
    Guid Id,
    string Email,
    string Rol,
    bool EsActivo);
