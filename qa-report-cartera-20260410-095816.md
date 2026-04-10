# QA Report - Happy Path Cartera

- Fecha: 2026-04-10 09:58:54
- Base URL: https://localhost:7099/api/cartera

## Paso 1 - POST /api/cartera/miembros

- HTTP Status: 401
- Error: The remote server returned an error: (401) Unauthorized.

### Payload
```json
{
    "documentoIdentidad":  "CC-QA-TEST-001",
    "fechaIngreso":  "2026-03-29",
    "apellidos":  "Villamizar Test",
    "tipoMiembro":  2,
    "nombres":  "Daniel QA",
    "apodo":  "DQA"
}
```

### Response
```json
{}
```

## Paso 2 - POST /api/cartera/conceptos-cobro

- HTTP Status: 401
- Error: The remote server returned an error: (401) Unauthorized.

### Payload
```json
{
    "periodicidadMensual":  1,
    "nombre":  "Cuota Mensual Test",
    "valorCOP":  150000,
    "cuentaContableIngresoId":  "550e8400-e29b-41d4-a716-446655440000"
}
```

### Response
```json
{}
```

## Paso 3 - POST /api/cartera/cuentas-por-cobrar

- HTTP Status: 
- Error: Dependencias previas fallaron

### Payload
```json
{}
```

### Response
```json
{}
```

## Resumen

- Exitosos (201): 0/3
