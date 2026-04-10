using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Common;
using LAMAMedellin.Domain.Entities;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.CrearConceptoCobro;

public sealed class CrearConceptoCobroCommandHandler(
    IConceptoCobroRepository conceptoCobroRepository,
    ICuentaContableRepository cuentaContableRepository)
    : IRequestHandler<CrearConceptoCobroCommand, Guid>
{
    public async Task<Guid> Handle(CrearConceptoCobroCommand request, CancellationToken cancellationToken)
    {
        // Validar que la cuenta contable existe
        var cuentaContable = await cuentaContableRepository.GetByIdAsync(
            request.CuentaContableIngresoId,
            cancellationToken);

        if (cuentaContable is null)
        {
            throw new ReglaNegocioException(
                $"La cuenta contable con Id {request.CuentaContableIngresoId} no existe.");
        }

        var conceptoCobro = new ConceptoCobro(
            request.Nombre,
            request.ValorCOP,
            request.PeriodicidadMensual,
            request.CuentaContableIngresoId);

        await conceptoCobroRepository.AddAsync(conceptoCobro, cancellationToken);
        await conceptoCobroRepository.SaveChangesAsync(cancellationToken);

        return conceptoCobro.Id;
    }
}
