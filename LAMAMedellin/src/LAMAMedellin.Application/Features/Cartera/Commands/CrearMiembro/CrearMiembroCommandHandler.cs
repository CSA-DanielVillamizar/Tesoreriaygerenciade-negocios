using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.CrearMiembro;

public sealed class CrearMiembroCommandHandler(
    IMiembroRepository miembroRepository)
    : IRequestHandler<CrearMiembroCommand, Guid>
{
    public async Task<Guid> Handle(CrearMiembroCommand request, CancellationToken cancellationToken)
    {
        var miembro = new Miembro(
            request.DocumentoIdentidad,
            request.Nombres,
            request.Apellidos,
            request.Apodo,
            request.FechaIngreso,
            request.TipoMiembro);

        await miembroRepository.AddAsync(miembro, cancellationToken);
        await miembroRepository.SaveChangesAsync(cancellationToken);

        return miembro.Id;
    }
}
