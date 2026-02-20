Deployment Guide

Instructions for deploying the LAMA Medellín system to production.

## Pre-Deployment Checklist

Before deploying to any environment, ensure:

- [ ] All tests passing (`dotnet test`)
- [ ] Code reviewed and approved
- [ ] Database migrations reviewed and tested
- [ ] No hardcoded secrets in code
- [ ] Configuration for target environment prepared
- [ ] Backup of current production data created
- [ ] Deployment rollback plan documented
- [ ] Team notifications sent

## Environments

**Development**
- Local developer machines
- SQL Server LocalDB or Docker
- Azure Key Vault: Development instance
- Testing via Postman/REST Client

**Staging**
- Azure App Service (staging slot)
- Azure SQL test database
- Azure Key Vault: Staging instance
- User Acceptance Testing (UAT)

**Production**
- Azure App Service (main slot)
- Azure SQL production database
- Azure Key Vault: Production instance
- Live user data

## Build & Package

### Local Build

```powershell
cd src/LAMAMedellin

# Run tests
dotnet test

# Build for release
dotnet build -c Release

# Package for deployment
dotnet publish -c Release -o ./publish
```

### Docker Build (if using containers)

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/LAMAMedellin.API/", "LAMAMedellin.API/"]
# ... copy other projects
RUN dotnet build -c Release

FROM base
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LAMAMedellin.API.dll"]
```

## Deployment Steps

### 1. Prepare Environment Variables

Create `appsettings.Production.json`:

```json
{
  "ConnectionStrings": {
    "LAMAMedellinDb": "Server=tcp:production-server.database.windows.net;Database=LAMAMedellin;Authentication=Active Directory Managed Identity;Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "Authentication": {
    "Authority": "https://login.microsoftonline.com/{TENANT-ID}"
  }
}
```

### 2. Database Migration

On target environment:

```powershell
cd src/LAMAMedellin.API

# Review pending migrations
dotnet ef migrations has-pending-changes --project ../LAMAMedellin.Infrastructure

# Apply migrations to production
dotnet ef database update --project ../LAMAMedellin.Infrastructure

# Verify schema applied correctly
dotnet ef migrations list --project ../LAMAMedellin.Infrastructure
```

### 3. Deploy API

**Option A: Azure App Service (Recommended)**

```powershell
# Login to Azure
az login

# Publish to App Service
az webapp deployment source config-zip --resource-group <rg-name> `
  --name <app-service-name> --src ./publish.zip

# Restart app service
az webapp restart --resource-group <rg-name> --name <app-service-name>

# Monitor deployment logs
az webapp log tail --resource-group <rg-name> --name <app-service-name>
```

**Option B: Docker to Azure Container Registry**

```powershell
# Build image
docker build -t lama-meddellin:1.0 .

# Login to ACR
az acr login --name <registry-name>

# Tag image
docker tag lama-meddellin:1.0 <registry-name>.azurecr.io/lama-meddellin:1.0

# Push to ACR
docker push <registry-name>.azurecr.io/lama-meddellin:1.0

# Deploy via Azure Container Instances or App Service
```

### 4. Verify Deployment

```powershell
# Health check
curl https://api.lama-medellin.com/health

# Check logs in Application Insights
az monitor app-insights query --app <insights-name> --analytics-query "
  customEvents
  | where name == 'ApplicationStartup'
  | project timestamp, message
"

# Database connectivity test
curl https://api.lama-medellin.com/api/test/database
```

### 5. Post-Deployment Verification

- [ ] API responding to requests (HTTP 200)
- [ ] Database queries executing correctly
- [ ] No errors in Application Insights
- [ ] Authentication working (token validation)
- [ ] Authorization working (role-based access)
- [ ] Key business transactions working
- [ ] Performance acceptable (API response times)

## Database Backup & Rollback

### Pre-Deployment Backup

```powershell
# Backup current production database
az sql db export --resource-group <rg-name> `
  --server <server-name> --name LAMAMedellin `
  --admin-user <admin> --admin-password <password> `
  --storage-key <key> --storage-key-type StorageAccessKey `
  --storage-uri "https://<storage>.blob.core.windows.net/<container>/"
```

### Rollback Procedure

If deployment fails:

1. **Revert Code**
   ```powershell
   git revert <bad-commit>
   # Rebuild and deploy previous version
   ```

2. **Rollback Database**
   ```powershell
   # Restore from backup
   az sql db restore --resource-group <rg-name> `
     --server <server-name> --name LAMAMedellin `
     --time <backup-time>
   ```

3. **Verify System**
   - Confirm API responding
   - Confirm data integrity
   - Monitor for any issues

## Monitoring & Alerting

### Application Insights Setup

```http
GET https://api.lama-medellin.com/health
X-Application-insights: {instrumentation-key}
```

### Key Metrics to Monitor

- API Response Time (target: <500ms p95)
- Request Count (baseline tracking)
- Error Rate (target: <0.1%)
- Database Connection Pool Usage
- Disk/Memory Usage

### Alerting Rules

- Error Rate > 1% → Page on-call
- Response Time p95 > 2s → Page on-call
- Database CPU > 80% → Notify team
- Failed Database Connections → Page on-call

## Production Runbook

### Daily Tasks
- [ ] Check Application Insights for errors
- [ ] Monitor performance metrics
- [ ] Review transaction volumes

### Weekly Tasks
- [ ] Review error logs
- [ ] Check backup completion
- [ ] Verify security patches

### Monthly Tasks
- [ ] Performance analysis
- [ ] Database statistics update
- [ ] Plan maintenance windows

## Common Issues

**API Fails to Start**
- Check connection string in Key Vault
- Verify database migrations applied
- Review Application logs

**Database Connection Timeout**
- Check firewall rules
- Verify connection string
- Check database status in Azure Portal

**High Response Times**
- Check database query execution plans
- Monitor server resources (CPU, memory, I/O)
- Review Application Insights for slow queries

## Deployment Timeline

Typical production deployment:

1. **Pre-deployment** (30 min): Backup, verify tests, notify team
2. **Deployment** (10 min): Code + migrations pushed
3. **Verification** (15 min): Health checks, smoke tests
4. **Post-deployment** (15 min): Monitoring, user notification

Total: ~70 minutes

## For More Information

- [Backend Setup](BACKEND-SETUP.md) - Architecture details
- [Architecture Overview](ARCHITECTURE.md) - System design
- [Azure Documentation](https://docs.microsoft.com/en-us/azure/)
- [Entity Framework Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)

---

Questions? Create an [issue](https://github.com/CSA-DanielVillamizar/Tesoreriaygerenciade-negocios/issues/new) or contact the DevOps team.
