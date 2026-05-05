# L.A.M.A. ERP - Sistema de Gestion de Moto Clubs

L.A.M.A. ERP es una plataforma integral para la gestion operativa, financiera y administrativa del Capitulo L.A.M.A. Medellin. Centraliza procesos criticos del club en una sola solucion, con enfoque en trazabilidad, control y escalabilidad empresarial.

## Modulos Principales

1. Cartera

- Gestion de conceptos de cobro, cuentas por cobrar, cuotas y seguimiento de recaudos pendientes.

1. Tesoreria

- Control de cajas, ingresos, egresos y saldos disponibles para operacion diaria.

1. Merchandising

- Administracion de inventario, ventas y movimiento de productos del club.

1. Miembros

- Directorio de miembros, perfil ampliado, datos de emergencia y estado activo del capitulo.

1. Eventos

- Agenda de eventos y rodadas, detalle por evento y control de asistencia.

## Arquitectura

### Backend

- .NET 8 Web API
- Clean Architecture
- CQRS con MediatR
- Entity Framework Core
- Azure SQL

### Frontend

- Next.js (App Router)
- TypeScript
- React Query (TanStack Query)
- TailwindCSS

## Ejecucion Local

### Backend (.NET 8)

```bash
cd LAMAMedellin
dotnet build
dotnet run --project src/LAMAMedellin.API
```

### Frontend (Next.js)

```bash
cd frontend
npm install
npm run dev
```

## Construido con orgullo

Construido con orgullo para fortalecer la gestion institucional de L.A.M.A. Medellin.
