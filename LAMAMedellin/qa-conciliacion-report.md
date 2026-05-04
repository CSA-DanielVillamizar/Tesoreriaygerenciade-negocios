# QA Conciliacion Automatica

- Fecha: 2026-05-04 09:53:35
- Base URL: http://localhost:7099

- Token expira: 2026-05-04 10:36:19
## Setup
- Miembro: Araque Betancur (53b72c14-609b-458b-9f5a-18e117277771)
- Concepto: Cuota Mensual Test (d183ffa2-c100-4f61-9077-5e0ab421ddc7)
- Cuenta por cobrar creada: 0298f165-27c5-4b90-9c7e-5f4a513b6542
- Caja usada: e9ab5e35-5360-4fc4-b957-405c0be26399
- Saldo inicial caja: 1000000.00

## Validacion Tesoreria
- Saldo esperado caja: 1040000.00
- Saldo obtenido caja: 1040000.00
- Resultado Tesoreria: PASS

## Validacion Cartera
- Saldo pendiente esperado: 60000
- Saldo pendiente obtenido: 60000.00
- Resultado Cartera: PASS

## Resultado Final
- PASS: Cartera y Tesoreria cruzaron informacion correctamente (conciliacion automatica OK).
