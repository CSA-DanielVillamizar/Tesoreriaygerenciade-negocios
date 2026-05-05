using LAMAMedellin.Application.Common.Exceptions;
using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Usuarios.Commands.AsignarRol;

public sealed class AsignarRolCommandHandler(IUsuarioRepository usuarioRepository)
    : IRequestHandler<AsignarRolCommand, Unit>
{
    public async Task<Unit> Handle(AsignarRolCommand request, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.GetByIdAsync(request.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            throw new ExcepcionNegocio("El usuario indicado no existe.");
        }

        usuario.AsignarRol(request.NuevoRol);
        await usuarioRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
