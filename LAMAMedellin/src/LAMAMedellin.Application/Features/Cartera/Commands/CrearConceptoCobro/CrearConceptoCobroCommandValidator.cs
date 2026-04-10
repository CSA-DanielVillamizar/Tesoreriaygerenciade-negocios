using FluentValidation;

namespace LAMAMedellin.Application.Features.Cartera.Commands.CrearConceptoCobro;

public sealed class CrearConceptoCobroCommandValidator : AbstractValidator<CrearConceptoCobroCommand>
{
    public CrearConceptoCobroCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("Nombre es obligatorio.")
            .MaximumLength(150).WithMessage("Nombre no puede exceder 150 caracteres.");

        RuleFor(x => x.ValorCOP)
            .GreaterThan(0).WithMessage("ValorCOP debe ser mayor a cero.");

        RuleFor(x => x.PeriodicidadMensual)
            .GreaterThan(0).WithMessage("PeriodicidadMensual debe ser mayor a cero.");

        RuleFor(x => x.CuentaContableIngresoId)
            .NotEmpty().WithMessage("CuentaContableIngresoId es obligatorio.");
    }
}
