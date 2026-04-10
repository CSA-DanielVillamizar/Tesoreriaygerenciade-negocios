#!/bin/bash
# Azure Blob Storage Setup para Merchandising L.A.M.A.
# Ejecutar desde PowerShell/Bash con permisos de Azure

set -e

SUBSCRIPTION_ID="a90e8a4a-74dd-4f34-bd61-24e59885a3ac"
RESOURCE_GROUP="rg-lamaregionnorte-prod"
STORAGE_ACCOUNT_NAME="stalamamedellinprod"
LOCATION="centralus"
CONTAINER_NAME="merchandising-imagenes"
APP_SERVICE_NAME="app-lamamedellin-backend-prod"

echo "==============================================="
echo "Azure Blob Storage Setup for Merchandising"
echo "==============================================="
echo ""

echo "[1/7] Verificar suscripción..."
CURRENT_SUB=$(az account show --query name -o tsv)
echo "✓ Suscripción actual: $CURRENT_SUB"
echo ""

echo "[2/7] Verificar Resource Group..."
RG_EXISTS=$(az group exists --name "$RESOURCE_GROUP")
if [ "$RG_EXISTS" = "true" ]; then
  echo "✓ Resource Group encontrado: $RESOURCE_GROUP"
else
  echo "✗ Resource Group NO encontrado"
  exit 1
fi
echo ""

echo "[3/7] Crear Storage Account..."
STORAGE_EXISTS=$(az storage account check-name --name "$STORAGE_ACCOUNT_NAME" --query nameAvailable -o tsv)
if [ "$STORAGE_EXISTS" = "false" ]; then
  echo "ℹ Storage Account ya existe (reutilizando)"
else
  echo "⏳ Creando Storage Account..."
  az storage account create \
    --name "$STORAGE_ACCOUNT_NAME" \
    --resource-group "$RESOURCE_GROUP" \
    --location "$LOCATION" \
    --sku "Standard_LRS" \
    --allow-blob-public-access true \
    --https-only true \
    --kind "StorageV2" \
    --output none
  echo "✓ Storage Account creado"
fi
echo ""

echo "[4/7] Obtener Connection String..."
CONNECTION_STRING=$(az storage account show-connection-string \
  --name "$STORAGE_ACCOUNT_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --query connectionString -o tsv)
if [ -z "$CONNECTION_STRING" ]; then
  echo "✗ Error: No se pudo obtener Connection String"
  exit 1
fi
echo "✓ Connection String obtenida"
echo "  (Primeros 80 chars): ${CONNECTION_STRING:0:80}..."
echo ""

echo "[5/7] Crear Contenedor..."
CONTAINER_EXISTS=$(az storage container exists \
  --name "$CONTAINER_NAME" \
  --connection-string "$CONNECTION_STRING" \
  --query exists -o tsv)
if [ "$CONTAINER_EXISTS" = "true" ]; then
  echo "ℹ Contenedor ya existe (reutilizando)"
else
  echo "⏳ Creando contenedor..."
  az storage container create \
    --name "$CONTAINER_NAME" \
    --public-access blob \
    --connection-string "$CONNECTION_STRING" \
    --output none
  echo "✓ Contenedor creado"
fi
echo ""

echo "[6/7] Inyectar Connection String en App Service..."
echo "⏳ Actualizando settings del App Service..."
az webapp config appsettings set \
  --name "$APP_SERVICE_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --settings "ConnectionStrings__BlobStorage=$CONNECTION_STRING" \
  --output none
echo "✓ Connection String inyectada"
echo ""

echo "[7/7] Verificación Final..."
BLOB_URL="https://${STORAGE_ACCOUNT_NAME}.blob.core.windows.net/${CONTAINER_NAME}/"
echo "✓ Storage Account: $STORAGE_ACCOUNT_NAME"
echo "✓ Contenedor: $CONTAINER_NAME"
echo "✓ Public URL: $BLOB_URL"
echo "✓ App Service: $APP_SERVICE_NAME (Connection String configurada)"
echo ""

echo "==============================================="
echo "✅ CONFIGURACIÓN COMPLETADA"
echo "==============================================="
echo ""
echo "🚀 Próximos pasos:"
echo "  1. Redeploy del App Service para cargar los nuevos settings"
echo "  2. El backend ya puede subir imágenes a los productos"
echo "  3. Las imágenes será accesibles públicamente desde:"
echo "     $BLOB_URL"
echo ""
