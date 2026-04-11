using LAMAMedellin.Application.Common.Exceptions;
using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.RegistrarPagoCartera;

public sealed class RegistrarPagoCarteraCommandHandler(
    ICuentaPorCobrarRepository cuentaPorCobrarRepository)
    : IRequestHandler<RegistrarPagoCarteraCommand, Unit>
{
    public async Task<Unit> Handle(RegistrarPagoCarteraCommand request, CancellationToken cancellationToken)
    {
        var cuenta = await cuentaPorCobrarRepository.GetByIdAsync(request.CuentaPorCobrarId, cancellationToken);
        if (cuenta is null)
        {
            throw new ExcepcionNegocio("La cuenta por cobrar indicada no existe.");
        }

        cuenta.AplicarPago(request.Monto);
        await cuentaPorCobrarRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
