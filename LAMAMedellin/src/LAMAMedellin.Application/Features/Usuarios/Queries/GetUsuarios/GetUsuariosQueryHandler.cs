using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Usuarios.Queries.GetUsuarios;

public sealed class GetUsuariosQueryHandler(IUsuarioRepository usuarioRepository)
    : IRequestHandler<GetUsuariosQuery, IReadOnlyList<UsuarioDto>>
{
    public async Task<IReadOnlyList<UsuarioDto>> Handle(GetUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await usuarioRepository.GetAllAsync(cancellationToken);

        return usuarios
            .Select(usuario => new UsuarioDto(
                usuario.Id,
                usuario.Email,
                usuario.Rol.ToString(),
                usuario.EsActivo))
            .ToList();
    }
}
