using LAMAMedellin.Domain.Common;
using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Domain.Entities;

public sealed class TarifaCuota : BaseEntity
{
    public TipoAfiliacion TipoAfiliacion { get; private set; }
    public decimal ValorMensualCOP { get; private set; }

#pragma warning disable CS8618
    private TarifaCuota() { }
#pragma warning restore CS8618

    public TarifaCuota(TipoAfiliacion tipoAfiliacion, decimal valorMensualCop)
    {
        if (valorMensualCop < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(valorMensualCop), "ValorMensualCOP no puede ser negativo.");
        }

        TipoAfiliacion = tipoAfiliacion;
        ValorMensualCOP = valorMensualCop;
    }

    public void ActualizarValorMensual(decimal valorMensualCop)
    {
        if (valorMensualCop < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(valorMensualCop), "ValorMensualCOP no puede ser negativo.");
        }

        ValorMensualCOP = valorMensualCop;
    }
}
