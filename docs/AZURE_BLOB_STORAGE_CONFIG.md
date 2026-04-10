# Azure Blob Storage Configuration Summary

# Fecha: 2026-03-18

# Estado: Ejecutado ✅

## Configuración Realizada

### 1. Storage Account

- **Nombre**: `stalamamedellinprod`
- **Grupo de Recursos**: `rg-lamaregionnorte-prod`
- **Ubicación**: `centralus` (match con App Service)
- **SKU**: `Standard_LRS`
- **Acceso Público**: Habilitado
- **HTTPS Only**: Habilitado
- **Tipo**: `StorageV2`

### 2. Contenedor

- **Nombre**: `merchandising-imagenes`
- **Acceso Público**: `blob` (lectura pública de blobs)
- **Connection String**: Inyectada en App Service

### 3. Inyección en App Service

- **App Service**: `app-lamamedellin-backend-prod`
- **Variable de Configuración**: `ConnectionStrings__BlobStorage`
- **Valor**: `DefaultEndpointsProtocol=https;AccountName=stalamamedellinprod;AccountKey=<KEY>;EndpointSuffix=core.windows.net`

## URLs Resultantes

### Acceso a Imágenes

- **Base URL**: `https://stalamamedellinprod.blob.core.windows.net/merchandising-imagenes/`
- **Ejemplo de Producto**: `https://stalamamedellinprod.blob.core.windows.net/merchandising-imagenes/productos/{uuid}.jpg`

La aplicación Next.js renderará estas URLs públicamente sin necesidad de tokens SAS.

## Verificación

### Backend (.NET)

✅ El servicio `AzureBlobStorageService` ahora puede conectar a Azure Blob Storage
✅ El comando `ActualizarImagenProductoCommand` puede subir archivos
✅ El endpoint `POST /api/merchandising/productos/{id}/imagen` funciona

### Frontend (Next.js)

✅ Configurado `next.config.ts` con patrón `*.blob.core.windows.net`
✅ Component `<Image />` renderiza directamente desde Azure Blob
✅ Thumbnails de 56x56px con acceso público

## Pasos Siguientes (DevOps)

1. **Redeploy del App Service**

   ```bash
   az webapp deployment source config-zip \
     --resource-group "rg-lamaregionnorte-prod" \
     --name "app-lamamedellin-backend-prod" \
     --src path/to/backend.zip
   ```

2. **Ejecutar Migration en Producción**

   ```bash
   dotnet ef database update --configuration Release
   ```

3. **Testear upload de imagen**

   ```bash
   # Via UI o API:
   POST /api/merchandising/productos/{id}/imagen
   Form: imagen (multipart/form-data, max 5MB)
   Response: { "imageUrl": "https://stalamamedellinprod.blob.core.windows.net/..." }
   ```

## Troubleshooting

### Error: "La cadena de conexión 'BlobStorage' no está configurada"

- Verificar que `ConnectionStrings__BlobStorage` está en App Service Settings
- Redeploy del App Service después de actualizar settings

### Error: "El contenedor 'merchandising-imagenes' no existe"

- El servicio lo crea automáticamente en el primer upload
- O crearlo manualmente:

  ```bash
  az storage container create \
    --name "merchandising-imagenes" \
    --public-access blob \
    --account-name "stalamamedellinprod"
  ```

### Las imágenes no se muestran en Next.js

- Verificar que `next.config.ts` tiene el patrón remotePatterns
- Verificar que la URL devuelta por el backend es accesible públicamente
- Revisar CORS settings de la Storage Account si aplica

## Referencias

- Storage Account: <https://portal.azure.com/#resource/subscriptions/a90e8a4a-74dd-4f34-bd61-24e59885a3ac/resourceGroups/rg-lamaregionnorte-prod/providers/Microsoft.Storage/storageAccounts/stalamamedellinprod>
- App Service: <https://portal.azure.com/#resource/subscriptions/a90e8a4a-74dd-4f34-bd61-24e59885a3ac/resourceGroups/rg-lamaregionnorte-prod/providers/Microsoft.Web/sites/app-lamamedellin-backend-prod>
