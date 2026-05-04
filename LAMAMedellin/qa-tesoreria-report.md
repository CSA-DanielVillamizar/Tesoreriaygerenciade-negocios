# QA Tesoreria

- Fecha: 2026-05-04 09:34:57
- Base URL: http://localhost:7099

- Token expira: 2026-05-04 10:36:19
## Setup

- Caja seleccionada: e9ab5e35-5360-4fc4-b957-405c0be26399
- Saldo inicial: 970000.00
- CentroCostoId: 827c4627-5e2f-4fb5-8ad7-9387ed9d1a0c
- CuentaIngresoId: 5c2bd49a-3279-4ba4-91ee-768facad8d6c
- CuentaEgresoId: 4ef41e86-7029-4636-9047-f988375d11fa

## Prueba 1 - Suma (Ingreso 50,000 COP)
- Status POST: 201
- Saldo esperado: 1020000.00
- Saldo obtenido: 1020000.00
- Resultado: PASS

## Prueba 2 - Resta (Egreso 20,000 COP)
- Status POST: 201
- Saldo esperado: 1000000.00
- Saldo obtenido: 1000000.00
- Resultado: PASS

## Prueba 3 - Fondos Insuficientes
- Status POST: 400
- Resultado: PASS
- Respuesta: {"type":"https://httpstatuses.com/400","title":"Regla de negocio no cumplida","status":400,"detail":"Saldo insuficiente en caja para registrar el egreso.","instance":"/api/tesoreria/egresos"}

## Resultado Final
- PASS: Las tres pruebas de Tesoreria pasaron correctamente.
