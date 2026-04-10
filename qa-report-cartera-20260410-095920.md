# QA Report - Happy Path Cartera

- Fecha: 2026-04-10 10:00:19
- Base URL: https://localhost:7099/api/cartera

## Paso 1 - POST /api/cartera/miembros

- HTTP Status: 201

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
{"id":"f2108e8e-99da-46f8-bd57-7ae98d211f3c"}
```

## Paso 2 - POST /api/cartera/conceptos-cobro

- HTTP Status: 500
- Error: The remote server returned an error: (500) Internal Server Error.

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
{"type":"https://httpstatuses.com/500","title":"Error interno del servidor","status":500,"detail":"Ocurrió un error inesperado.","instance":"/api/cartera/conceptos-cobro"}
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

- Exitosos (201): 1/3
