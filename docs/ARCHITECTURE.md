System Architecture

High-level system design and technical approach for LAMA Medellín.

## Architecture Overview

The system is built using Clean Architecture pattern with distinct layers, ensuring separation of concerns and maintainability.

```
┌─────────────────────────────────────────────────────────┐
│                   API Layer (HTTP)                       │
│              (LAMAMedellin.API - Controllers)            │
└────────────────┬────────────────────────────────────────┘
                 │
┌────────────────┴────────────────────────────────────────┐
│         Application Layer (CQRS + Orchestration)        │
│  (LAMAMedellin.Application - Commands, Queries, DTOs)   │
└────────────────┬────────────────────────────────────────┘
                 │
┌────────────────┴──────────────────┬─────────────────────┐
│   Infrastructure Layer             │                     │
│   (Data Access, External APIs)     │                     │
│   (LAMAMedellin.Infrastructure)    │ Depends on:         │
└────────────────┬──────────────────┴─────────────────────┘
                 │
┌────────────────┴───────────────────────────────────────┐
│        Domain Layer (Business Logic)                    │
│  (LAMAMedellin.Domain - Entities, Interfaces)           │
│  NO external framework dependencies                      │
└──────────────────────────────────────────────────────────┘
```

## Layer Responsibilities

### Domain (Bottom - Dependencies Only)
- **Entities**: Business objects with unique identity (TransaccionBancaria, Cuota, Banco)
- **Value Objects**: Immutable, dependency-free values (Monto, CuentaBancaria)
- **Interfaces**: Contracts only (ITransaccionRepository, ICuotaService)
- **Constants**: Business rules and enumerations
- **No framework references**: Pure business logic, testable without infrastructure

### Application (Middle - Business Process Orchestration)
- **Commands**: Write operations (RegistrarCuotaCommand, AnularTransaccion)
- **Queries**: Read operations (ObtenerSaldoBanco, ListarCuotas)
- **Handlers**: Business logic execution via MediatR
- **DTOs**: Data structures for API communication
- **Validators**: Input validation using FluentValidation
- **Mappers**: Transform Domain entities to DTOs
- **Depends on**: Domain only

### Infrastructure (Middle - Technical Implementation)
- **DbContext**: Entity Framework Core data model
- **Repositories**: Implement domain repository interfaces
- **Services**: External API clients, Azure services
- **Configuration**: EF migrations, service setup
- **Depends on**: Domain (via interfaces)

### API (Top - HTTP Entry Point)
- **Controllers**: Handle HTTP requests/responses
- **Middleware**: Cross-cutting concerns (logging, error handling, auth)
- **Routes**: REST endpoint mappings
- **Depends on**: Application + Infrastructure

## Data Flow

### Command/Write Operation Example

```
User Request
    ↓
API Controller receives HTTP POST
    ↓
Controller creates RegistrarCuotaCommand
    ↓
MediatR dispatcher receives command
    ↓
Application finds RegistrarCuotaHandler
    ↓
Handler validates input (FluentValidation)
    ↓
Handler calls domain: Cuota.Crear()
    ↓
Domain validates business rules
    ↓
Domain returns new Cuota entity
    ↓
Handler calls IRepository.AgregarAsync(cuota)
    ↓
Infrastructure: Repository saves to DB via EF Core
    ↓
DB commits transaction
    ↓
Handler returns success result (cuotaId)
    ↓
Controller returns 201 Created with Location header
    ↓
User receives HTTP 201 with cuota ID
```

### Query/Read Operation Example

```
User Request
    ↓
API Controller receives HTTP GET /cuotas?centrocosto=CC001
    ↓
Controller creates ObtenerCuotasQuery
    ↓
MediatR dispatcher receives query
    ↓
Application finds ObtenerCuotasHandler
    ↓
Handler calls IRepository.ObtenerPorCentroCostoAsync()
    ↓
Infrastructure: Repository queries DB via EF Core
    ↓
Handler maps Domain entities to DTOs
    ↓
Handler returns DTO collection
    ↓
Controller returns HTTP 200 with JSON array
    ↓
User receives cuota list
```

## Core Patterns

### Clean Architecture
- Dependencies flow inward (API → Domain, never Domain → API)
- Business logic isolated in Domain and Application
- Infrastructure is interchangeable (can swap EF Core for Dapper without breaking logic)
- Testable: Domain can be tested without database

### CQRS (Command Query Responsibility Segregation)
- Commands: RegistrarCuotaCommand, AnularTransaccion, AbrirCuenta
- Queries: ObtenerSaldoBanco, ListarCuotas, ConsultarMovimientos
- Separation enables independent scaling and optimization
- MediatR handles command/query dispatch

### Dependency Injection
- All dependencies injected into constructors
- Configured in `Program.cs` (API layer)
- Enables testing via mocking
- Loose coupling between components

### Repository Pattern
- Interface in Domain (ITransaccionRepository)
- Implementation in Infrastructure (TransaccionRepository)
- Enables testing with in-memory repositories
- Hides EF Core from business logic

### Exception Handling
- Domain exceptions: Business rule violations
- Infrastructure exceptions: Technical failures
- Middleware catches all exceptions and returns ProblemDetails
- Never let exceptions leak to client as unformatted text

## Key Business Rules

**Mandatory Fields**
- Every monetary transaction must have CentroCostoId
- Transactions always include transaction date
- Users authenticated via Microsoft Entra External ID (no local users)

**Soft Deletes**
- Financial records never physically deleted
- IsDeleted flag marks records as inactive
- Queries always filter `WHERE IsDeleted = 0`
- Enables audit trail and recovery

**Multimoneda**
- Functional currency: COP (Colombian Peso)
- When USD used: track MontoMonedaOrigen, TasaCambioUsada, FechaTasaCambio, FuenteTasaCambio
- Single currency in financial statements (converted to COP)

**Auditability**
- All changes tracked (CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
- Transaction history immutable (reversals, not edits)
- Soft deletes retain historical data for audit

## Technology Decisions

**Why Clean Architecture**
- Domain logic stays testable and framework-independent
- Easy to replace infrastructure (DB migration, API framework change)
- Clear responsibility boundaries
- Team members understand code organization immediately

**Why CQRS**
- Separates read and write models for optimization
- Commands encapsulate business operations
- Queries can be optimized independently
- Better traceability of operations

**Why Entity Framework Core**
- Mature, widely-used ORM for .NET
- Object-relational mapping reduces boilerplate
- LINQ queries provide type safety
- Migrations enable version control of schema

**Why MediatR**
- Decouples controllers from handlers
- Single Responsibility Principle
- Easy to add cross-cutting concerns (validation, logging, authorization)
- Command/Query pattern becomes explicit

## Deployment Architecture

```
GitHub Repository
    ↓
    ├── CI/CD Pipeline (GitHub Actions - future)
    ├─── Build & Test
    └─── Deploy to Azure App Service
    
Azure Infrastructure
    ├── App Service: API hosting
    ├── SQL Server: Relational database
    ├── Key Vault: Secrets management
    ├── Application Insights: Monitoring/logging
    └── Blob Storage: File uploads
```

## Security Architecture

- Authentication: Microsoft Entra External ID (100% delegated)
- Authorization: Claims-based roles (no local role tables)
- Secrets: Azure Key Vault (never in appsettings)
- Encryption: TLS for transport, Azure SQL encryption at rest
- Audit: All data changes logged with user context

## Performance Considerations

- API responses cached where appropriate
- Database queries optimized with indexes
- Entity Framework eager loading for common queries
- Pagination for large result sets
- Async/await for I/O operations

## For More Information

- [Backend Setup Guide](BACKEND-SETUP.md) - Implementation details
- [Code Standards](../governance/process-docs/CODE-STANDARDS.md) - Coding conventions
- [Contributing Guide](guides/CONTRIBUTING.md) - Development workflow
