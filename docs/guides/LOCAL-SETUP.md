Local Development Setup

Get your development environment up and running.

## Prerequisites

- Windows 10/11 or macOS/Linux with WSL
- .NET SDK 8.0 or higher ([Download](https://dotnet.microsoft.com/download))
- Visual Studio Code (recommended) with C# extension
- PowerShell 5.1 or higher
- Git 2.30 or higher
- Azure storage account (optional, for local development)

## Environment Setup

### 1. Clone the Repository

```powershell
git clone https://github.com/CSA-DanielVillamizar/Tesoreriaygerenciade-negocios.git
cd Tesoreriaygerenciade-negocios
```

### 2. Backend Setup

Navigate to the backend solution:
```powershell
cd src/LAMAMedellin
```

Restore dependencies:
```powershell
dotnet restore
```

Build the solution:
```powershell
dotnet build
```

### 3. Database Configuration

Create a local SQL Server database or use Azure SQL emulator:

**Option A: SQL Server Emulator**
Follow Microsoft documentation to install and run SQL Server on Docker or locally.

**Option B: Azure SQL**
Create a free Azure SQL test database and configure connection string.

Create `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "LAMAMedellinDb": "Server=localhost;Database=LAMAMedellin;Integrated Security=true;Encrypt=false;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

### 4. Run Migrations

Apply database migrations:
```powershell
cd src/LAMAMedellin.API
dotnet ef database update --project ../LAMAMedellin.Infrastructure
```

### 5. Start Development Server

From `src/LAMAMedellin`:
```powershell
dotnet run --project src/LAMAMedellin.API
```

API will be available at: `http://localhost:5001`

## Development Tools

### Recommended VS Code Extensions

- **C# Dev Kit** (ms-dotnettools.csharp)
- **C# Extensions** (ms-dotnettools.vscode-dotnet-runtime)
- **REST Client** (humao.rest-client) - test API endpoints
- **GitLens** (eamodio.gitlens) - git integration
- **Markdown All in One** (yzhang.markdown-all-in-one) - documentation editing

### Useful Commands

```powershell
# Clean build artifacts
dotnet clean

# Run tests
dotnet test

# Format code
dotnet format

# Check for issues (requires roslyn analyzers)
dotnet build --no-incremental

# View project details
dotnet project-info
```

## Project Structure

```
LAMAMedellin/
├── LAMAMedellin.slnx         Solution file
└── src/
    ├── LAMAMedellin.Domain/           Business logic
    ├── LAMAMedellin.Application/      Use cases
    ├── LAMAMedellin.Infrastructure/   Data access
    └── LAMAMedellin.API/              HTTP endpoints
```

Details in [Backend Setup](../../docs/BACKEND-SETUP.md)

## Workflow

### 1. Create Feature Branch

```bash
git checkout -b feature/issue-123-description
```

### 2. Develop

- Make changes following [Code Standards](../../governance/process-docs/CODE-STANDARDS.md)
- Add tests alongside code
- Run tests frequently

### 3. Test Locally

```powershell
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "TestName"

# Run with coverage
dotnet test /p:CollectCoverage=true
```

### 4. Commit & Push

```bash
git add .
git commit -m "feat: Brief description (#123)"
git push origin feature/issue-123-description
```

### 5. Create Pull Request

Push to GitHub and create a PR with:
- Clear title
- Description of changes
- Reference to issue: `Fixes #123`
- Testing notes

## Troubleshooting

**Build fails with "project file does not exist"**
- Ensure you're in the correct directory: `src/LAMAMedellin`
- Run: `dotnet sln LAMAMedellin.slnx find` to auto-discover projects

**Database connection fails**
- Check connection string in `appsettings.json`
- Verify SQL Server is running
- Check firewall settings for local connections

**Tests fail after pulling new changes**
- Run: `dotnet clean && dotnet restore`
- Reapply migrations: `dotnet ef database update`

**Port 5001 already in use**
- Change port in `launchSettings.json`
- Or stop other processes using port 5001

## Additional Resources

- [Backend Setup Guide](../../docs/BACKEND-SETUP.md)
- [Code Standards](../../governance/process-docs/CODE-STANDARDS.md)
- [Contributing Guide](CONTRIBUTING.md)
- [.NET 8 Documentation](https://learn.microsoft.com/en-us/dotnet/core/)
- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)

---

Questions? Create an [issue](https://github.com/CSA-DanielVillamizar/Tesoreriaygerenciade-negocios/issues/new) or check existing documentation.
