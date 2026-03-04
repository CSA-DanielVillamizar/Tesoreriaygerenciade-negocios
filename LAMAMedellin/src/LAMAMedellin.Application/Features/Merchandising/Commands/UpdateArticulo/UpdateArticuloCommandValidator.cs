using FluentValidation;

namespace LAMAMedellin.Application.Features.Merchandising.Commands.UpdateArticulo;

public sealed class UpdateArticuloCommandValidator : AbstractValidator<UpdateArticuloCommand>
{
    public UpdateArticuloCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.SKU).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descripcion).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Categoria).IsInEnum();
        RuleFor(x => x.PrecioVenta).GreaterThan(0);
        RuleFor(x => x.CostoPromedio).GreaterThanOrEqualTo(0);
        RuleFor(x => x.StockActual).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CuentaContableIngresoId).NotEmpty();
    }
}
