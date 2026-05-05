using MediatR;

namespace LAMAMedellin.Application.Features.Usuarios.Commands.SyncUsuario;

public sealed record SyncUsuarioCommand(
    string Email,
    string EntraObjectId,
    string Nombres) : IRequest<SyncUsuarioResponseDto>;
