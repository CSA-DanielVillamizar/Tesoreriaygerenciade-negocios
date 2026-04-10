# Ejecutar estos comandos desde Azure CLI o PowerShell para completar la configuración
# Copiar y pegar cada comando completamente.

# ========================================
# PASO 1: Crear Storage Account
# ========================================
az storage account create `
    --name "stalamamedellinprod" `
    --resource-group "rg-lamaregionnorte-prod" `
    --location "centralus" `
    --sku "Standard_LRS" `
    --allow-blob-public-access true `
    --https-only true `
    --kind "StorageV2"

# ========================================
# PASO 2: Obtener Connection String
# ========================================
$connStr = az storage account show-connection-string `
    --name "stalamamedellinprod" `
    --resource-group "rg-lamaregionnorte-prod" `
    --query connectionString -o tsv

Write-Host "Connection String: $connStr"

# ========================================
# PASO 3: Crear Contenedor
# ========================================
az storage container create `
    --name "merchandising-imagenes" `
    --public-access blob `
    --connection-string $connStr

# ========================================
# PASO 4: Inyectar en App Service
# ========================================
az webapp config appsettings set `
    --name "app-lamamedellin-backend-prod" `
    --resource-group "rg-lamaregionnorte-prod" `
    --settings "ConnectionStrings__BlobStorage=$connStr"

# ========================================
# PASO 5: Verificar Configuración
# ========================================
Write-Host "Verificando..."
az webapp config appsettings list `
    --name "app-lamamedellin-backend-prod" `
    --resource-group "rg-lamaregionnorte-prod" | `
    Select-String "ConnectionStrings__BlobStorage"

Write-Host "✅ Configuración completada"
