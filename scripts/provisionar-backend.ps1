param(
    [string]$ResourceGroup = "rg-lamaregionnorte-prod",
    [string]$Location = "eastus",
    [string]$AppServicePlanName = "plan-lamaregionnorte-prod",
    [string]$WebAppName = "app-lamaregionnorte-backend-prod",
    [string]$SqlUser = "<SQL_USER>",
    [string]$SqlPassword = "<SQL_PASSWORD>"
)

$ErrorActionPreference = "Stop"

Write-Host "Creando App Service Plan (Linux, B1) en $ResourceGroup..." -ForegroundColor Cyan
az appservice plan create `
  --name $AppServicePlanName `
  --resource-group $ResourceGroup `
  --location $Location `
  --sku B1 `
  --is-linux

Write-Host "Creando Web App (.NET 8) en $ResourceGroup..." -ForegroundColor Cyan
az webapp create `
  --name $WebAppName `
  --resource-group $ResourceGroup `
  --plan $AppServicePlanName `
  --runtime "DOTNETCORE|8.0"

$connectionString = "Server=tcp:lamaregionnorte-sql-a90e.database.windows.net,1433;Initial Catalog=LAMAMedellinContable;Persist Security Info=False;User ID=$SqlUser;Password=$SqlPassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

Write-Host "Configurando ConnectionString DefaultConnection en Web App..." -ForegroundColor Cyan
az webapp config connection-string set `
  --name $WebAppName `
  --resource-group $ResourceGroup `
  --connection-string-type SQLAzure `
  --settings DefaultConnection="$connectionString"

Write-Host "Provisionamiento completado." -ForegroundColor Green
Write-Host "Plan: $AppServicePlanName" -ForegroundColor Green
Write-Host "Web App: $WebAppName" -ForegroundColor Green
