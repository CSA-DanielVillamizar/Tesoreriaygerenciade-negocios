param(
    [string]$ResourceGroup = "rg-lamamedellin-prod",
    [string]$Location = "eastus",
    [string]$AppServicePlanName = "plan-lamamedellin-prod",
    [string]$WebAppName = "app-lamamedellin-backend-prod",
    [string]$SqlServerName = "sql-lamamedellin-prod",
    [string]$SqlDatabaseName = "sqldb-lamamedellin-prod",
    [string]$SqlAdminUser,
    [string]$SqlAdminPassword,
    [ValidateSet("Basic", "S0")]
    [string]$SqlSku = "Basic"
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($SqlAdminUser) -or [string]::IsNullOrWhiteSpace($SqlAdminPassword)) {
    throw "Debes enviar -SqlAdminUser y -SqlAdminPassword para crear Azure SQL Server."
}

Write-Host "Validando sesion de Azure CLI..." -ForegroundColor Cyan
az account show --output none
if ($LASTEXITCODE -ne 0) { throw "No hay sesion activa en Azure CLI." }

Write-Host "Creando/actualizando Resource Group '$ResourceGroup'..." -ForegroundColor Cyan
az group create --name $ResourceGroup --location $Location --output none | Out-Null
if ($LASTEXITCODE -ne 0) { throw "No fue posible crear/actualizar el Resource Group." }

Write-Host "Creando/actualizando App Service Plan Linux B1 '$AppServicePlanName'..." -ForegroundColor Cyan
az appservice plan create --name $AppServicePlanName --resource-group $ResourceGroup --location $Location --sku B1 --is-linux --output none | Out-Null
if ($LASTEXITCODE -ne 0) { throw "No fue posible crear/actualizar el App Service Plan." }

Write-Host "Creando/actualizando Web App .NET 8 '$WebAppName'..." -ForegroundColor Cyan
$webAppExists = $false
try {
    az webapp show --name $WebAppName --resource-group $ResourceGroup --output none | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "webapp show fallo" }
    $webAppExists = $true
}
catch {
    $webAppExists = $false
}

if (-not $webAppExists) {
    az webapp create --name $WebAppName --resource-group $ResourceGroup --plan $AppServicePlanName --runtime "DOTNETCORE:8.0" --output none | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "No fue posible crear la Web App." }
}
else {
    az webapp config set --name $WebAppName --resource-group $ResourceGroup --linux-fx-version "DOTNETCORE|8.0" --output none | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "No fue posible actualizar runtime de la Web App." }
}

Write-Host "Creando/actualizando SQL logical server '$SqlServerName'..." -ForegroundColor Cyan
$sqlServerExists = $false
try {
    az sql server show --name $SqlServerName --resource-group $ResourceGroup --output none | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "sql server show fallo" }
    $sqlServerExists = $true
}
catch {
    $sqlServerExists = $false
}

if (-not $sqlServerExists) {
    az sql server create --name $SqlServerName --resource-group $ResourceGroup --location $Location --admin-user $SqlAdminUser --admin-password $SqlAdminPassword --output none | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "No fue posible crear el SQL Server." }
}

Write-Host "Configurando regla de firewall AzureServices (0.0.0.0)..." -ForegroundColor Cyan
az sql server firewall-rule create --name AllowAzureServices --resource-group $ResourceGroup --server $SqlServerName --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0 --output none | Out-Null
if ($LASTEXITCODE -ne 0) { throw "No fue posible configurar firewall rule AllowAzureServices." }

Write-Host "Creando/actualizando SQL Database '$SqlDatabaseName' ($SqlSku)..." -ForegroundColor Cyan
az sql db create --name $SqlDatabaseName --resource-group $ResourceGroup --server $SqlServerName --service-objective $SqlSku --backup-storage-redundancy Local --output none | Out-Null
if ($LASTEXITCODE -ne 0) { throw "No fue posible crear/actualizar la SQL Database." }

Write-Host "Habilitando Identidad Administrada (System-Assigned) en Web App..." -ForegroundColor Cyan
az webapp identity assign --name $WebAppName --resource-group $ResourceGroup --output none | Out-Null
if ($LASTEXITCODE -ne 0) { throw "No fue posible habilitar la identidad administrada en la Web App." }

$managedIdentityConnectionString = "Server=tcp:$SqlServerName.database.windows.net,1433;Initial Catalog=$SqlDatabaseName;Authentication=Active Directory Default;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

Write-Host "Configurando ConnectionStrings__DefaultConnection para Managed Identity..." -ForegroundColor Cyan
az webapp config connection-string set --name $WebAppName --resource-group $ResourceGroup --connection-string-type SQLAzure --settings "DefaultConnection=$managedIdentityConnectionString" --output none | Out-Null
if ($LASTEXITCODE -ne 0) { throw "No fue posible configurar DefaultConnection en Web App." }

Write-Host "Provisionamiento completado." -ForegroundColor Green
Write-Host "Resource Group: $ResourceGroup" -ForegroundColor Green
Write-Host "App Service Plan: $AppServicePlanName" -ForegroundColor Green
Write-Host "Web App: $WebAppName" -ForegroundColor Green
Write-Host "SQL Server: $SqlServerName" -ForegroundColor Green
Write-Host "SQL Database: $SqlDatabaseName" -ForegroundColor Green
Write-Host "SQL Firewall: AllowAzureServices (0.0.0.0)" -ForegroundColor Green
