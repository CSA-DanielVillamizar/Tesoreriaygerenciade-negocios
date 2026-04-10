# Script de configuración de Azure Blob Storage para Merchandising
# Autor: Azure DevOps Architect
# Fecha: 2026-03-18

$ErrorActionPreference = "Stop"
$logFile = "C:\Users\DanielVillamizar\Sistema Contable L.A.M.A. Medellin\scripts\setup-azure-blob-storage.log"

# Limpiar log anterior
"" | Out-File $logFile -Encoding UTF8

Write-Host "=== CONFIGURACIÓN DE AZURE BLOB STORAGE PARA MERCHANDISING ===" -ForegroundColor Cyan
"=== CONFIGURACIÓN DE AZURE BLOB STORAGE PARA MERCHANDISING ===" | Tee-Object -FilePath $logFile -Append
Write-Host ""

# Variables de configuración
$subscriptionId = "a90e8a4a-74dd-4f34-bd61-24e59885a3ac"
$resourceGroup = "rg-lamaregionnorte-prod"
$storageAccountName = "stalamamedellinprod"
$location = "centralus"
$containerName = "merchandising-imagenes"
$appServiceName = "app-lamamedellin-backend-prod"

Write-Host "📋 Variables de configuración:" -ForegroundColor Yellow
Write-Host "  Suscripción: $subscriptionId"
Write-Host "  Grupo de Recursos: $resourceGroup"
Write-Host "  Nombre Storage Account: $storageAccountName"
Write-Host "  Ubicación: $location"
Write-Host "  Contenedor: $containerName"
Write-Host "  App Service: $appServiceName"
Write-Host ""

# Paso 1: Verificar contexto de Azure
Write-Host "📍 PASO 1: Verificando contexto de Azure..." -ForegroundColor Green
$currentAccount = az account show --output json | ConvertFrom-Json
Write-Host "  ✓ Suscripción actual: $($currentAccount.name)"
Write-Host "  ✓ Usuario: $($currentAccount.user.name)"
Write-Host ""

# Paso 2: Verificar que el Resource Group existe
Write-Host "📍 PASO 2: Verificando Resource Group..." -ForegroundColor Green
$rg = az group show --name $resourceGroup --output json 2>$null | ConvertFrom-Json
if ($rg) {
    Write-Host "  ✓ Resource Group encontrado: $($rg.location)"
} else {
    Write-Host "  ✗ Resource Group no encontrado" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Paso 3: Crear Storage Account
Write-Host "📍 PASO 3: Creando Storage Account '$storageAccountName'..." -ForegroundColor Green
try {
    $storageCheck = az storage account show --name $storageAccountName --resource-group $resourceGroup --output json 2>$null | ConvertFrom-Json
    if ($storageCheck) {
        Write-Host "  ℹ Storage Account ya existe (reutilizando)"
    } else {
        Write-Host "  ⏳ Creando Storage Account..."
        az storage account create `
            --name $storageAccountName `
            --resource-group $resourceGroup `
            --location $location `
            --sku "Standard_LRS" `
            --allow-blob-public-access true `
            --https-only true `
            --kind "StorageV2" `
            --output none
        Write-Host "  ✓ Storage Account creado exitosamente"
    }
} catch {
    Write-Host "  ✗ Error al crear Storage Account: $_" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Paso 4: Obtener Connection String
Write-Host "📍 PASO 4: Obteniendo Connection String..." -ForegroundColor Green
try {
    $connectionString = az storage account show-connection-string `
        --name $storageAccountName `
        --resource-group $resourceGroup `
        --output tsv

    if ($connectionString) {
        Write-Host "  ✓ Connection String obtenida:"
        Write-Host "    (Primera 80 caracteres: $($connectionString.Substring(0, [Math]::Min(80, $connectionString.Length)))...)"
    } else {
        throw "No se pudo obtener la Connection String"
    }
} catch {
    Write-Host "  ✗ Error al obtener Connection String: $_" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Paso 5: Crear contenedor
Write-Host "📍 PASO 5: Creando contenedor '$containerName'..." -ForegroundColor Green
try {
    # Verificar si existe
    $containerExists = az storage container exists `
        --name $containerName `
        --connection-string $connectionString `
        --output json | ConvertFrom-Json

    if ($containerExists.exists) {
        Write-Host "  ℹ Contenedor ya existe (reutilizando)"
    } else {
        Write-Host "  ⏳ Creando contenedor..."
        az storage container create `
            --name $containerName `
            --public-access blob `
            --connection-string $connectionString `
            --output none
        Write-Host "  ✓ Contenedor creado exitosamente"
    }
} catch {
    Write-Host "  ✗ Error al crear contenedor: $_" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Paso 6: Inyectar Connection String en App Service
Write-Host "📍 PASO 6: Inyectando Connection String en App Service '$appServiceName'..." -ForegroundColor Green
try {
    Write-Host "  ⏳ Actualizando Application Settings..."
    az webapp config appsettings set `
        --name $appServiceName `
        --resource-group $resourceGroup `
        --settings "ConnectionStrings__BlobStorage=$connectionString" `
        --output none
    Write-Host "  ✓ Connection String inyectada exitosamente"
} catch {
    Write-Host "  ✗ Error al inyectar Connection String: $_" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Paso 7: Verificación final
Write-Host "📍 PASO 7: Verificación final..." -ForegroundColor Green
try {
    $appSettings = az webapp config appsettings list `
        --name $appServiceName `
        --resource-group $resourceGroup `
        --output json | ConvertFrom-Json

    $blobSetting = $appSettings | Where-Object { $_.name -eq "ConnectionStrings__BlobStorage" }
    if ($blobSetting) {
        Write-Host "  ✓ Connection String está configurada en App Service"
    } else {
        Write-Host "  ⚠ Advertencia: Connection String no encontrada en App Service" -ForegroundColor Yellow
    }

    # Verificar Storage Account
    $storage = az storage account show `
        --name $storageAccountName `
        --resource-group $resourceGroup `
        --output json | ConvertFrom-Json

    Write-Host "  ✓ Storage Account Status: $($storage.properties.provisioningState)"

    # Verificar contenedor
    $container = az storage container exists `
        --name $containerName `
        --connection-string $connectionString `
        --output json | ConvertFrom-Json

    Write-Host "  ✓ Contenedor existe: $($container.exists)"
} catch {
    Write-Host "  ⚠ Error durante verificación: $_" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "✅ CONFIGURACIÓN COMPLETADA" -ForegroundColor Green
Write-Host ""
Write-Host "📊 Resumen:" -ForegroundColor Cyan
Write-Host "  • Storage Account: $storageAccountName (Ubicación: $location)"
Write-Host "  • Contenedor: $containerName (Acceso: blob público)"
Write-Host "  • Connection String: Inyectada en $appServiceName"
Write-Host ""
Write-Host "⚡ Próximos pasos:" -ForegroundColor Yellow
Write-Host "  1. Redeploy el App Service para que cargue la nueva Connection String"
Write-Host "  2. El backend ahora puede subir imágenes a Azure Blob Storage"
Write-Host "  3. Las imágenes estarán disponibles en: https://$storageAccountName.blob.core.windows.net/$containerName/"
Write-Host ""
