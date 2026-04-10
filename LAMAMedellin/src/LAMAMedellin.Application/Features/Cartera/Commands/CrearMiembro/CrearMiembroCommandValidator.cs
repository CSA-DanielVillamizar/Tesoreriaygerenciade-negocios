using FluentValidation;

namespace LAMAMedellin.Application.Features.Cartera.Commands.CrearMiembro;

public sealed class CrearMiembroCommandValidator : AbstractValidator<CrearMiembroCommand>
{
    public CrearMiembroCommandValidator()
    {
        RuleFor(x => x.DocumentoIdentidad)
            .NotEmpty().WithMessage("DocumentoIdentidad es obligatorio.")
            .MaximumLength(30).WithMessage("DocumentoIdentidad no puede exceder 30 caracteres.");

        RuleFor(x => x.Nombres)
            .NotEmpty().WithMessage("Nombres es obligatorio.")
            .MaximumLength(100).WithMessage("Nombres no puede exceder 100 caracteres.");

        RuleFor(x => x.Apellidos)
            .NotEmpty().WithMessage("Apellidos es obligatorio.")
            .MaximumLength(100).WithMessage("Apellidos no puede exceder 100 caracteres.");

        RuleFor(x => x.Apodo)
            .MaximumLength(120).WithMessage("Apodo no puede exceder 120 caracteres.");

        RuleFor(x => x.FechaIngreso)
            .NotEmpty().WithMessage("FechaIngreso es obligatoria.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("FechaIngreso no puede ser mayor a hoy.");

        RuleFor(x => x.TipoMiembro)
            .IsInEnum().WithMessage("TipoMiembro debe ser un valor válido.");
    }
}
