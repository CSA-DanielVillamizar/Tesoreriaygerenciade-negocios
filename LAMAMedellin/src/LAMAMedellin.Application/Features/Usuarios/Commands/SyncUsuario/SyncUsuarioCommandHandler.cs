using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Usuarios.Commands.SyncUsuario;

public sealed class SyncUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
    : IRequestHandler<SyncUsuarioCommand, SyncUsuarioResponseDto>
{
    public async Task<SyncUsuarioResponseDto> Handle(SyncUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.GetByEntraObjectIdAsync(request.EntraObjectId, cancellationToken);

        if (usuario is null)
        {
            usuario = new Usuario(
                request.Email,
                request.EntraObjectId,
                RolSistema.Logistica,
                true,
                null);

            await usuarioRepository.AddAsync(usuario, cancellationToken);
            await usuarioRepository.SaveChangesAsync(cancellationToken);
        }

        return new SyncUsuarioResponseDto(usuario.Id, usuario.Rol.ToString());
    }
}
