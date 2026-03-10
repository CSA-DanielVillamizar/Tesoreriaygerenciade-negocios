using FluentValidation;

namespace LAMAMedellin.Application.Features.Configuracion.Tarifas.Commands.ActualizarTarifasCuota;

public sealed class ActualizarTarifasCuotaCommandValidator : AbstractValidator<ActualizarTarifasCuotaCommand>
{
    public ActualizarTarifasCuotaCommandValidator()
    {
        RuleFor(x => x.Tarifas)
            .NotEmpty();

        RuleForEach(x => x.Tarifas).ChildRules(item =>
        {
            item.RuleFor(t => t.TipoAfiliacion)
                .IsInEnum();

            item.RuleFor(t => t.ValorMensualCOP)
                .GreaterThanOrEqualTo(0);
        });

        RuleFor(x => x.Tarifas)
            .Must(tarifas => tarifas.Select(x => x.TipoAfiliacion).Distinct().Count() == tarifas.Count)
            .WithMessage("No se permiten tipos de afiliación duplicados.");
    }
}
