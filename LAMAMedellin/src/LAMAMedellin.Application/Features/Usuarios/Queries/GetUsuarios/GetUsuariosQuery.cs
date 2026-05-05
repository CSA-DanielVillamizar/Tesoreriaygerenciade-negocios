using MediatR;

namespace LAMAMedellin.Application.Features.Usuarios.Queries.GetUsuarios;

public sealed record GetUsuariosQuery : IRequest<IReadOnlyList<UsuarioDto>>;
