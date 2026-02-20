Contributing to LAMA Medellín

Thank you for contributing to the LAMA Medellín Accounting System! This guide will help you make meaningful contributions.

## Getting Started

1. Fork the repository
2. Clone your fork locally
3. Follow [Local Development Setup](LOCAL-SETUP.md)
4. Create a feature branch

## Development Process

### 1. Pick an Issue

- Check [GitHub Issues](https://github.com/CSA-DanielVillamizar/Tesoreriaygerenciade-negocios/issues)
- Look for issues labeled `good-first-issue` or `help-wanted`
- Comment to claim the issue before starting work

### 2. Create a Feature Branch

```bash
git checkout -b feature/issue-123-brief-description
```

Branch naming convention:
- `feature/issue-XXX-description` for new features
- `fix/issue-XXX-description` for bug fixes
- `docs/issue-XXX-description` for documentation

### 3. Make Your Changes

Follow these principles:

**Code Style**
- Read [Code Standards](../governance/process-docs/CODE-STANDARDS.md)
- Business logic in Spanish, infrastructure in English
- No emojis anywhere
- Run linter/formatter before committing

**Architecture**
- Respect Clean Architecture layers
- Put business logic in Domain/Application, not API
- Use dependency injection
- Write CQRS commands/queries for operations

**Testing**
- Add unit tests for new logic
- Test added methods cover happy path and edge cases
- Integration tests for data layer changes
- Run all tests before pushing

### 4. Commit Your Changes

```bash
# Commit with clear message
git commit -m "feat: Add cuota registration feature (#123)

- Implement RegistrarCuotaCommand and handler
- Add domain entity validation
- Create API endpoint for registration
- Add unit tests for command handler
"
```

Commit format:
- Scope: `feat:` (feature), `fix:` (bug), `docs:` (documentation), `refactor:` (code style)
- Reference issue number: `(#123)`
- Describe what was done and why

### 5. Push and Create Pull Request

```bash
git push origin feature/issue-123-brief-description
```

Then create a pull request on GitHub with:
- Clear title: "feat: Add cuota registration feature"
- Description explaining the change
- Reference to related issue: `Fixes #123`
- Checklist of what you've done (testing, documentation, etc.)

### 6. Code Review

- Respond to feedback promptly
- Make requested changes in new commits (don't squash)
- Mark conversations as resolved when addressed
- Request re-review after changes

### 7. Merge

Once approved, maintainers will merge your PR. Your feature is now live!

## Guidelines

### Writing Code

**Clean Architecture**
```
Domain/          <- Business entities and rules
├── Entities/
├── ValueObjects/
├── Interfaces/   <- Contracts only
└── Constants/

Application/     <- Use cases and orchestration
├── Commands/
├── Queries/
├── DTOs/
├── Validators/
└── Handlers/

Infrastructure/  <- Data access and external services
├── Persistence/ <- EF Core DbContext, repositories
└── Services/

API/             <- HTTP endpoints
├── Controllers/
└── Middleware/
```

**CQRS Pattern**
- Commands for writes (RegistrarCuota, AnularTransaccion)
- Queries for reads (ObtenerSaldoBanco, ListarCuotas)
- Handlers contain all business logic
- Use MediatR to dispatch commands/queries

**Error Handling**
```csharp
// Domain exceptions for business rules
throw new DomainException("Monto debe ser positivo");

// Infrastructure exceptions for technical issues
throw new PersistenceException("No se pudo guardar en BD");
```

### Documentation

**Code Comments**
- Explain "why", not "what"
- Use XML comments for public APIs
- Keep comments up-to-date

```csharp
/// <summary>
/// Calcula la mora sobre una cuota vencida
/// </summary>
/// <param name="díasVencida">Días de retraso</param>
/// <returns>Monto de mora calculado</returns>
public decimal CalcularMora(int díasVencida)
{
    // Mora simple: 2% mensual = 0.0667% diario
    return montoCuota * (díasVencida * 0.000667m);
}
```

**Commit Messages**
- Use imperative: "Add feature" not "Added feature"
- Reference issues: "Fixes #123"
- Explain the "why" if it's not obvious

### Testing

**Structure**
```
tests/
└── LAMAMedellin.Application.Tests/
    └── Commands/
        └── RegistrarCuotaCommandHandlerTests.cs
```

**Test Example**
```csharp
[Fact]
public async Task Handle_ConMontoPositivo_DebeRetornarId()
{
    // Arrange
    var command = new RegistrarCuotaCommand 
    { 
        Monto = 100000,
        CentroCostoId = "CC001"
    };
    
    // Act
    var id = await handler.Handle(command, CancellationToken.None);
    
    // Assert
    id.Should().NotBeEmpty();
    repositoryMock.Verify(x => x.AgregarAsync(It.IsAny<Cuota>()), Times.Once);
}
```

### Language

**Spanish (Domain)**
```csharp
public class RegistrarCuotaCommand { }
public interface ICuotaRepository { }
public class TransaccionBancaria { }
```

**English (Infrastructure)**
```csharp
public class AzureBlobStorageService { }
public class EntityTypeConfiguration { }
public interface ILogger { }
```

## Code Review Checklist

Before submitting, ensure:
- [ ] Code follows [Code Standards](../governance/process-docs/CODE-STANDARDS.md)
- [ ] Unit tests added and passing
- [ ] No hardcoded secrets or sensitive data
- [ ] Commit messages are clear
- [ ] PR description is complete
- [ ] Documentation updated if needed
- [ ] No emojis in code
- [ ] CentroCostoId validated if monetary operation
- [ ] Soft deletes used instead of physical deletion

## Questions?

- Check existing [GitHub Issues](https://github.com/CSA-DanielVillamizar/Tesoreriaygerenciade-negocios/issues)
- Read [Code Standards](../governance/process-docs/CODE-STANDARDS.md)
- Review [Backend Setup](../../docs/BACKEND-SETUP.md)
- Create an [issue](https://github.com/CSA-DanielVillamizar/Tesoreriaygerenciade-negocios/issues/new) with `question` label

---

Happy coding!
