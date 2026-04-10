using FluentValidation;

namespace LAMAMedellin.Application.Features.Cartera.Commands.CrearCuentaPorCobrar;

public sealed class CrearCuentaPorCobrarCommandValidator : AbstractValidator<CrearCuentaPorCobrarCommand>
{
    public CrearCuentaPorCobrarCommandValidator()
    {
        RuleFor(x => x.MiembroId)
            .NotEmpty().WithMessage("MiembroId es obligatorio.");

        RuleFor(x => x.ConceptoCobroId)
            .NotEmpty().WithMessage("ConceptoCobroId es obligatorio.");

        RuleFor(x => x.FechaEmision)
            .NotEmpty().WithMessage("FechaEmision es obligatoria.");

        RuleFor(x => x.FechaVencimiento)
            .NotEmpty().WithMessage("FechaVencimiento es obligatoria.")
            .GreaterThanOrEqualTo(x => x.FechaEmision)
            .WithMessage("FechaVencimiento debe ser mayor o igual a FechaEmision.");

        RuleFor(x => x.ValorTotal)
            .GreaterThan(0).WithMessage("ValorTotal debe ser mayor a cero.");
    }
}
