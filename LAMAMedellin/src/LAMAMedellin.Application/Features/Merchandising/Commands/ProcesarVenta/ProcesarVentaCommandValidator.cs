using FluentValidation;

namespace LAMAMedellin.Application.Features.Merchandising.Commands.ProcesarVenta;

public sealed class ProcesarVentaCommandValidator : AbstractValidator<ProcesarVentaCommand>
{
    public ProcesarVentaCommandValidator()
    {
        RuleFor(x => x.NumeroFacturaInterna).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CentroCostoId).NotEmpty();
        RuleFor(x => x.MedioPago).IsInEnum();
        RuleFor(x => x.Detalles)
            .NotNull()
            .Must(x => x.Count > 0)
            .WithMessage("La venta debe contener al menos un detalle.");

        RuleForEach(x => x.Detalles).ChildRules(detalle =>
        {
            detalle.RuleFor(x => x.ArticuloId).NotEmpty();
            detalle.RuleFor(x => x.Cantidad).GreaterThan(0);
        });
    }
}
