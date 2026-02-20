using Server.DTOs.Dashboards;

namespace Server.Services.Interfaces;

/// <summary>
/// Interfaz para el servicio de Radar de Tesorería L.A.M.A.
/// Define el contrato para obtener datos consolidados del dashboard.
/// Implementa la capa de negocio siguiendo Clean Architecture.
/// </summary>
public interface IDashboardService
{
    /// <summary>
    /// Obtiene los datos completos del Radar de Tesorería de forma asincrónica.
    /// 
    /// Consolida información de:
    /// - CuentasFinancieras: Saldo en bancos y caja
    /// - Recibos: Recaudación del mes actual
    /// - Egresos: Gastos del mes actual
    /// - Deudores: Cartera por cobrar
    /// - Gráficos: Distribución de ingresos y comparativa semestral
    /// 
    /// Maneja conversiones USD → COP automáticamente para cuentas en dólares.
    /// </summary>
    /// <returns>Un DTO con todos los datos del dashboard listos para presentación.</returns>
    Task<DashboardDto> ObtenerRadarAsync();

    /// <summary>
    /// Obtiene el resumen histórico de un mes específico.
    /// 
    /// Calcula el saldo inicial acumulando movimientos previos al mes,
    /// y los ingresos/egresos del período seleccionado.
    /// </summary>
    /// <param name="anio">Año del período.</param>
    /// <param name="mes">Mes del período (1-12).</param>
    /// <returns>Un DTO con saldos y totales del mes solicitado.</returns>
    Task<DashboardDto> ObtenerResumenMensualAsync(int anio, int mes);
}
