# QA Report - Happy Path Cartera

- Fecha: 2026-04-10 10:47:00
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
{"id":"b1c8c46e-cfbd-46cc-9db9-b0b902183d57"}
```

## Paso 2 - POST /api/cartera/conceptos-cobro

- HTTP Status: 400
- Error: The remote server returned an error: (400) Bad Request.

### Payload
```json
{
    "periodicidadMensual":  1,
    "nombre":  "Cuota Mensual Test",
    "valorCOP":  150000,
    "cuentaContableIngresoId":  "550e8400-e29b-41d4-a716-446655440110"
}
```

### Response
```json
{"type":"https://httpstatuses.com/400","title":"Regla de negocio no cumplida","status":400,"detail":"La cuenta contable con Id 550e8400-e29b-41d4-a716-446655440110 no existe.","instance":"/api/cartera/conceptos-cobro"}
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
