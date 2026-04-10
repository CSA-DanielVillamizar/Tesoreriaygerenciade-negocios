$ErrorActionPreference = 'Stop'

Set-Location "c:\Users\DanielVillamizar\Sistema Contable L.A.M.A. Medellin"

$apiBase = 'https://app-lamamedellin-backend-prod.azurewebsites.net'
$scope = 'api://b81ee2ee-5417-4aa0-8000-e470aec5543e/.default'
$sqlServer = 'lamaregionnorte-sql-a90e.database.windows.net'
$sqlDb = 'LAMAMedellinContable'
$skuObjetivo = 'P-OFC-01'
$nombreProducto = 'Parche L.A.M.A. Oficial'
$precioVenta = 35000
$cantidadObjetivoAntesVenta = 10
$cantidadVenta = 2
$valorVentaEsperado = $precioVenta * $cantidadVenta

function Get-JsonValue {
    param(
        [Parameter(Mandatory = $true)] $Object,
        [Parameter(Mandatory = $true)] [string[]] $Names
    )

    foreach ($name in $Names) {
        if ($null -ne $Object.PSObject.Properties[$name]) {
            return $Object.$name
        }
    }

    return $null
}

$apiToken = az account get-access-token --scope $scope --query accessToken -o tsv
if ([string]::IsNullOrWhiteSpace($apiToken)) { throw 'No se pudo obtener token API.' }
$headers = @{ Authorization = "Bearer $apiToken"; 'Content-Type' = 'application/json' }

$sqlToken = az account get-access-token --resource https://database.windows.net/ --query accessToken -o tsv
if ([string]::IsNullOrWhiteSpace($sqlToken)) { throw 'No se pudo obtener token SQL.' }

$cuentaIngreso = Invoke-Sqlcmd -ServerInstance $sqlServer -Database $sqlDb -AccessToken $sqlToken -Query @"
SELECT TOP 1
    Id,
    Codigo,
    Descripcion,
    Naturaleza,
    PermiteMovimiento
FROM CuentasContables
WHERE IsDeleted = 0
  AND PermiteMovimiento = 1
  AND (Codigo = '410505' OR Codigo LIKE '4135%' OR Codigo LIKE '41%')
ORDER BY CASE WHEN Codigo = '410505' THEN 0 WHEN Codigo LIKE '4135%' THEN 1 ELSE 2 END, Codigo;
"@ | Select-Object -First 1
if (-not $cuentaIngreso) { throw 'No se encontró una cuenta contable de ingresos válida para merchandising.' }
$cuentaIngresoId = [Guid]$cuentaIngreso.Id

$centroCosto = Invoke-Sqlcmd -ServerInstance $sqlServer -Database $sqlDb -AccessToken $sqlToken -Query @"
SELECT TOP 1 Id, Nombre
FROM CentrosCosto
WHERE IsDeleted = 0
ORDER BY CASE WHEN Nombre LIKE '%Principal%' THEN 0 ELSE 1 END, Nombre;
"@ | Select-Object -First 1
if (-not $centroCosto) { throw 'No se encontró centro de costo.' }
$centroCostoId = [Guid]$centroCosto.Id

$cajasAntesCatalogo = Invoke-RestMethod -Method GET -Uri "$apiBase/api/tesoreria/cajas" -Headers $headers
$cajaGeneral = $cajasAntesCatalogo | Where-Object { (Get-JsonValue $_ @('nombre', 'Nombre')) -eq 'Caja General L.A.M.A.' } | Select-Object -First 1
if (-not $cajaGeneral) { throw 'No se encontró la Caja General L.A.M.A.' }
$cajaId = [Guid](Get-JsonValue $cajaGeneral @('id', 'Id'))
$saldoCajaAntes = [decimal](Get-JsonValue $cajaGeneral @('saldoActual', 'SaldoActual'))

$productosAntes = Invoke-RestMethod -Method GET -Uri "$apiBase/api/merchandising/productos" -Headers $headers
$producto = $productosAntes | Where-Object { (Get-JsonValue $_ @('sku', 'SKU')) -eq $skuObjetivo } | Select-Object -First 1
$productoCreado = $false

if (-not $producto) {
    $crearPayload = @{
        nombre                  = $nombreProducto
        sku                     = $skuObjetivo
        precioVentaCOP          = $precioVenta
        cuentaContableIngresoId = $cuentaIngresoId
    } | ConvertTo-Json

    $crearResponse = Invoke-RestMethod -Method POST -Uri "$apiBase/api/merchandising/productos" -Headers $headers -Body $crearPayload
    $productoId = [Guid](Get-JsonValue $crearResponse @('id', 'Id'))
    $productoCreado = $true

    $productosAntes = Invoke-RestMethod -Method GET -Uri "$apiBase/api/merchandising/productos" -Headers $headers
    $producto = $productosAntes | Where-Object { [Guid](Get-JsonValue $_ @('id', 'Id')) -eq $productoId } | Select-Object -First 1
}

if (-not $producto) { throw 'No fue posible obtener el producto de merchandising después del alta.' }

$productoId = [Guid](Get-JsonValue $producto @('id', 'Id'))
$stockAntes = [int](Get-JsonValue $producto @('cantidadStock', 'CantidadStock'))

if ($stockAntes -gt $cantidadObjetivoAntesVenta) {
    throw "El producto ya tiene stock $stockAntes, mayor al setup objetivo de $cantidadObjetivoAntesVenta. No es posible certificar exactamente stock final 8 sin contaminar la auditoría."
}

$cantidadEntrada = $cantidadObjetivoAntesVenta - $stockAntes
$movimientoEntradaId = $null
if ($cantidadEntrada -gt 0) {
    $entradaPayload = @{
        productoId    = $productoId
        cantidad      = $cantidadEntrada
        fecha         = (Get-Date).ToUniversalTime().ToString('o')
        observaciones = 'Fondeo inicial E2E merchandising producción'
    } | ConvertTo-Json

    $entradaResponse = Invoke-RestMethod -Method POST -Uri "$apiBase/api/merchandising/productos/$productoId/entradas" -Headers $headers -Body $entradaPayload
    $movimientoEntradaId = Get-JsonValue $entradaResponse @('id', 'Id')
}

$cajasAntesVenta = Invoke-RestMethod -Method GET -Uri "$apiBase/api/tesoreria/cajas" -Headers $headers
$cajaAntesVenta = $cajasAntesVenta | Where-Object { [Guid](Get-JsonValue $_ @('id', 'Id')) -eq $cajaId } | Select-Object -First 1
$saldoCajaAntesVenta = [decimal](Get-JsonValue $cajaAntesVenta @('saldoActual', 'SaldoActual'))

$ventaPayload = @{
    productoId    = $productoId
    cantidad      = $cantidadVenta
    cajaId        = $cajaId
    centroCostoId = $centroCostoId
    fecha         = (Get-Date).ToUniversalTime().ToString('o')
    observaciones = 'Primera venta oficial E2E merchandising producción'
} | ConvertTo-Json

$ventaResponse = Invoke-RestMethod -Method POST -Uri "$apiBase/api/merchandising/productos/$productoId/ventas" -Headers $headers -Body $ventaPayload
$comprobanteId = [Guid](Get-JsonValue $ventaResponse @('id', 'Id'))

$productosDespues = Invoke-RestMethod -Method GET -Uri "$apiBase/api/merchandising/productos" -Headers $headers
$productoDespues = $productosDespues | Where-Object { [Guid](Get-JsonValue $_ @('id', 'Id')) -eq $productoId } | Select-Object -First 1
if (-not $productoDespues) { throw 'No se pudo recuperar el producto después de la venta.' }
$stockDespues = [int](Get-JsonValue $productoDespues @('cantidadStock', 'CantidadStock'))

$cajasDespues = Invoke-RestMethod -Method GET -Uri "$apiBase/api/tesoreria/cajas" -Headers $headers
$cajaDespues = $cajasDespues | Where-Object { [Guid](Get-JsonValue $_ @('id', 'Id')) -eq $cajaId } | Select-Object -First 1
$saldoCajaDespues = [decimal](Get-JsonValue $cajaDespues @('saldoActual', 'SaldoActual'))
$deltaCaja = $saldoCajaDespues - $saldoCajaAntesVenta

$comprobanteSql = Invoke-Sqlcmd -ServerInstance $sqlServer -Database $sqlDb -AccessToken $sqlToken -Query @"
SELECT
    c.Id,
    c.NumeroConsecutivo,
    c.Fecha,
    c.TipoComprobante,
    c.Descripcion,
    c.EstadoComprobante
FROM Comprobantes c
WHERE c.Id = '$comprobanteId';
"@

$asientosSql = Invoke-Sqlcmd -ServerInstance $sqlServer -Database $sqlDb -AccessToken $sqlToken -Query @"
SELECT
    a.Id,
    a.ComprobanteId,
    cc.Codigo,
    cc.Descripcion AS CuentaDescripcion,
    a.Debe,
    a.Haber,
    a.Referencia
FROM AsientosContables a
INNER JOIN CuentasContables cc ON cc.Id = a.CuentaContableId
WHERE a.ComprobanteId = '$comprobanteId'
ORDER BY a.Debe DESC, a.Haber DESC, cc.Codigo;
"@

$reporte = [PSCustomObject]@{
    ProductoCreado           = $productoCreado
    ProductoId               = $productoId
    ProductoNombre           = $nombreProducto
    SKU                      = $skuObjetivo
    CuentaIngresoId          = $cuentaIngresoId
    CuentaIngresoCodigo      = $cuentaIngreso.Codigo
    CuentaIngresoDescripcion = $cuentaIngreso.Descripcion
    CentroCostoId            = $centroCostoId
    CentroCostoNombre        = $centroCosto.Nombre
    CajaId                   = $cajaId
    CajaNombre               = Get-JsonValue $cajaGeneral @('nombre', 'Nombre')
    SaldoCajaAntesVenta      = $saldoCajaAntesVenta
    SaldoCajaDespues         = $saldoCajaDespues
    DeltaCaja                = $deltaCaja
    DeltaEsperado            = $valorVentaEsperado
    StockAntesSetup          = $stockAntes
    CantidadEntradaAplicada  = $cantidadEntrada
    MovimientoEntradaId      = $movimientoEntradaId
    CantidadVendida          = $cantidadVenta
    StockDespuesVenta        = $stockDespues
    StockEsperado            = 8
    ComprobanteId            = $comprobanteId
}

'=== REPORTE CERTIFICACION E2E MERCHANDISING PROD ==='
$reporte | Format-List | Out-String | Write-Output
'=== COMPROBANTE SQL ==='
$comprobanteSql | Format-Table -AutoSize | Out-String | Write-Output
'=== ASIENTOS SQL ==='
$asientosSql | Format-Table -AutoSize | Out-String | Write-Output

if ($stockDespues -ne 8) {
    throw "Validación fallida: stock esperado 8 y se obtuvo $stockDespues."
}

if ($deltaCaja -ne $valorVentaEsperado) {
    throw "Validación fallida: delta caja esperado $valorVentaEsperado y se obtuvo $deltaCaja."
}
