# QA Report - Happy Path Cartera

- Fecha: 2026-04-10 10:29:08
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
{"id":"a7734cd1-d42c-4f99-aa6b-5cee9ed6ddaf"}
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
    "cuentaContableIngresoId":  "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"
}
```

### Response
```json
{"type":"https://httpstatuses.com/400","title":"Regla de negocio no cumplida","status":400,"detail":"La cuenta contable con Id a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d no existe.","instance":"/api/cartera/conceptos-cobro"}
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
