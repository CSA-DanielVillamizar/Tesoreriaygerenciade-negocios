namespace Server.DTOs.Presupuestos;

/// <summary>
/// DTO para estadísticas agregadas de presupuestos sin cargar todos los registros.
/// Utilizado para cálculos eficientes de totales, promedios y porcentajes.
/// </summary>
public class EstadisticasPresupuestoDto
{
    /// <summary>
    /// Suma total de montos presupuestados
    /// </summary>
    public decimal TotalPresupuestado { get; set; }

    /// <summary>
    /// Suma total de montos ejecutados (ingresos + egresos reales)
    /// </summary>
    public decimal TotalEjecutado { get; set; }

    /// <summary>
    /// Diferencia: TotalPresupuestado - TotalEjecutado
    /// </summary>
    public decimal Diferencia { get; set; }

    /// <summary>
    /// Porcentaje promedio de ejecución entre todos los presupuestos
    /// </summary>
    public decimal PorcentajeEjecucionPromedio { get; set; }

    /// <summary>
    /// Cantidad total de presupuestos en los filtros aplicados
    /// </summary>
    public int CantidadPresupuestos { get; set; }
}
