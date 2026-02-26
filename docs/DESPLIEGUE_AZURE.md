# Despliegue Backend a Azure

## 1) Provisionar infraestructura con Azure CLI

Desde la raíz del repositorio:

```powershell
az login
powershell -ExecutionPolicy Bypass -File .\scripts\provisionar-backend.ps1
```

Si deseas usar otros nombres (opcional):

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\provisionar-backend.ps1 `
  -ResourceGroup "rg-lamaregionnorte-prod" `
  -Location "eastus" `
  -AppServicePlanName "plan-lamaregionnorte-prod" `
  -WebAppName "app-lamaregionnorte-backend-prod" `
  -SqlUser "<SQL_USER>" `
  -SqlPassword "<SQL_PASSWORD>"
```

## 2) Obtener Publish Profile desde Azure

1. Abre el recurso **App Service** en Azure Portal.
2. En el menú del Web App, selecciona **Get publish profile** (Descargar perfil de publicación).
3. Se descargará un archivo `.PublishSettings`.

## 3) Configurar secreto en GitHub

1. En el repositorio: **Settings** -> **Secrets and variables** -> **Actions**.
2. Crea un secreto nuevo llamado: `AZURE_WEBAPP_PUBLISH_PROFILE`.
3. Pega el contenido completo del `.PublishSettings`.

Con esto, cada push a `main` con cambios en `LAMAMedellin/src/` disparará el workflow de despliegue del backend.
