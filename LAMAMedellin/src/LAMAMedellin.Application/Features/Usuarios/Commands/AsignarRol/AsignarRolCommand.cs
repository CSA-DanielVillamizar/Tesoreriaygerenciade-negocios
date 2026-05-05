using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Usuarios.Commands.AsignarRol;

public sealed record AsignarRolCommand(
    Guid UsuarioId,
    RolSistema NuevoRol) : IRequest<Unit>;
