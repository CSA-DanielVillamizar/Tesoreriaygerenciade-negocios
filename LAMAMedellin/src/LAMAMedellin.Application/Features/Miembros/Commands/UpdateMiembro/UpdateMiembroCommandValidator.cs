using FluentValidation;

namespace LAMAMedellin.Application.Features.Miembros.Commands.UpdateMiembro;

public sealed class UpdateMiembroCommandValidator : AbstractValidator<UpdateMiembroCommand>
{
    public UpdateMiembroCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.MarcaMoto)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ModeloMoto)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Cilindraje)
            .GreaterThan(0);

        RuleFor(x => x.Placa)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Rango)
            .IsInEnum();
    }
}
