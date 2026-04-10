# QA Report - Happy Path Cartera

- Fecha: 2026-04-10 10:49:46
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
{"id":"f646d2ac-9e4e-4985-b8f5-af5183546fbd"}
```

## Paso 2 - POST /api/cartera/conceptos-cobro

- HTTP Status: 201

### Payload
```json
{
    "periodicidadMensual":  1,
    "nombre":  "Cuota Mensual Test",
    "valorCOP":  150000,
    "cuentaContableIngresoId":  "4ef41e86-7029-4636-9047-f988375d11fa"
}
```

### Response
```json
{"id":"d183ffa2-c100-4f61-9077-5e0ab421ddc7"}
```

## Paso 3 - POST /api/cartera/cuentas-por-cobrar

- HTTP Status: 201

### Payload
```json
{
    "valorTotal":  150000,
    "fechaVencimiento":  "2026-04-29",
    "conceptoCobroId":  "d183ffa2-c100-4f61-9077-5e0ab421ddc7",
    "miembroId":  "f646d2ac-9e4e-4985-b8f5-af5183546fbd",
    "fechaEmision":  "2026-03-29"
}
```

### Response
```json
{"id":"498e34ba-259b-4cf4-9521-401c8862420b"}
```

## Resumen

- Exitosos (201): 3/3
