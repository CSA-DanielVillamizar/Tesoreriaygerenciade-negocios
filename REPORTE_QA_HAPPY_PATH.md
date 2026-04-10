
# 📋 REPORTE QA: Happy Path - Endpoints Cartera

## Status: ⚠️ API NO ESTÁ DISPONIBLE

**Fecha Test:** 2026-03-29
**Ingeniero QA:** Copilot
**Perfil:** Arquitecto Full Stack - Modo QA Testing

---

## 1. DIAGNÓSTICO INICIAL

### Verificación de Disponibilidad

| Puerto | Protocolo | Status      | Conclusión              |
|--------|-----------|-----------|--------------------------|
| 5006   | HTTP      | No Escuchando | API no disponible |
| 7015   | HTTPS     | **No Escuchando** | ⚠️ **API no iniciada** |

**Problema Identificado:** El servidor API no está corriendo en ninguno de los puertos configurados en `launchSettings.json`.

---

## 2. CONFIGURACIÓN ESPERADA

Según `launchSettings.json` (LAMAMedellin.API):

```json
{
  "https": {
    "applicationUrl": "https://localhost:7015;http://localhost:5006"
  }
}
```

**Endpoints Bajo Prueba:**

1. `POST /api/cartera/miembros` → Crear Miembro
2. `POST /api/cartera/conceptos-cobro` → Crear Concepto de Cobro
3. `POST /api/cartera/cuentas-por-cobrar` → Crear Cuenta por Cobrar

---

## 3. PASOS PARA EJECUTAR EL TEST

### Paso 1: Iniciar la API

Ejecuta en un terminal separado desde la carpeta del proyecto:

```bash
# Ir al directorio del proyecto
cd "c:\Users\DanielVillamizar\Sistema Contable L.A.M.A. Medellin\LAMAMedellin"

# Iniciar la API en perfil HTTPS (puerto 7015)
dotnet run --project src/LAMAMedellin.API/LAMAMedellin.API.csproj --configuration Development
```

**Señales de éxito esperadas:**

- ✅ `Now listening on: https://localhost:7015`
- ✅ `Now listening on: http://localhost:5006`
- ✅ `Application started. Press Ctrl+C to shut down.`

---

### Paso 2: Ejecutar el Script de Test (Una vez API esté corriendo)

```powershell
# En otro terminal, navegar al directorio del proyecto
cd "c:\Users\DanielVillamizar\Sistema Contable L.A.M.A. Medellin"

# Ejecutar el script de Happy Path
& ".\test-happy-path-cartera.ps1" -ApiPort 7015
```

---

## 4. ESTRUCTURA DEL TEST PROGRAMADO

### PASO 1: Crear Miembro (POST /api/cartera/miembros)

**Request Payload:**

```json
{
  "documentoIdentidad": "CC-123456789",
  "nombres": "Juan Carlos",
  "apellidos": "García López",
  "apodo": "JC",
  "fechaIngreso": "2026-03-29",
  "tipoMiembro": 2
}
```

**Respuesta Esperada:**

- **Status Code:** `201 Created`
- **Body:**

```json
{
  "id": "<GUID_MIEMBRO_GENERADO>"
}
```

**Datos Capturados:** `MiembroId` para uso en Paso 3

---

### PASO 2: Crear Concepto de Cobro (POST /api/cartera/conceptos-cobro)

**Request Payload:**

```json
{
  "nombre": "Cuota Mensual Test",
  "valorCOP": 150000,
  "periodicidadMensual": 1,
  "cuentaContableIngresoId": "550e8400-e29b-41d4-a716-446655440000"
}
```

**Respuesta Esperada:**

- **Status Code:** `201 Created`
- **Body:**

```json
{
  "id": "<GUID_CONCEPTO_GENERADO>"
}
```

**Datos Capturados:** `ConceptoCobroId` para uso en Paso 3

---

### PASO 3: Crear Cuenta por Cobrar (POST /api/cartera/cuentas-por-cobrar)

**Request Payload:** (usando IDs capturados de Pasos 1 y 2)

```json
{
  "miembroId": "<ID_DEL_PASO_1>",
  "conceptoCobroId": "<ID_DEL_PASO_2>",
  "fechaEmision": "2026-03-29",
  "fechaVencimiento": "2026-04-29",
  "valorTotal": 150000
}
```

**Respuesta Esperada:**

- **Status Code:** `201 Created`
- **Body:**

```json
{
  "id": "<GUID_CXC_GENERADO>"
}
```

---

## 5. CRITERIOS DE ÉXITO (Happy Path)

✅ **Happy Path Exitoso** = Los 3 pasos retornan `201 Created`

| Paso | Endpoint | Status Esperado | Resultado |
|------|----------|-----------------|-----------|
| 1 | POST /api/cartera/miembros | 201 Created | ⏳ Pendiente |
| 2 | POST /api/cartera/conceptos-cobro | 201 Created | ⏳ Pendiente |
| 3 | POST /api/cartera/cuentas-por-cobrar | 201 Created | ⏳ Pendiente |

---

## 6. NOTAS IMPORTANTES

### Autenticación

- Todos los endpoints están protegidos con `[Authorize]`
- El script automáticamente obtiene un token desde Azure AD
- Prerrequisito: Usuario con sesión activa en Azure CLI (`az login`)

### Consideraciones de Validación

- **Paso 1 (Miembro):** Valida DocumentoIdentidad es único
- **Paso 2 (Concepto):** Valida CuentaContableIngresoId existe en DB (ahora puede fallar si el GUID no existe)
- **Paso 3 (CxC):** Valida MiembroId y ConceptoCobroId existen (debe funcionar si Pasos 1 y 2 fueron exitosos)

### Errores Posibles en Ejecución

**Error 400 - Bad Request:**

- Payload malformado o tipo de dato incorrecto
- Validación FluentValidation fallida
- ID de CuentaContableIngreso no existe en BD

**Error 401 - Unauthorized:**

- Token de acceso inválido o expirado
- Ejecutar `az login` para renovar sesión

**Error 500 - Internal Server Error:**

- Exception en el handler (revisar Application Insights / logs)

---

## 7. CONCLUSIÓN ACTUAL

### ⚠️ Status: NO TESTEABLE

**Razón:** La API no está corriendo en ninguno de los puertos esperados (5006 o 7015).

**Próximos Pasos:**

1. ✅ Iniciar la API siguiendo instrucciones de Paso 1
2. ✅ Ejecutar el script: `test-happy-path-cartera.ps1`
3. ✅ Revisar las respuestas de cada endpoint
4. ✅ Documentar cualquier desviación del Happy Path

**Script Listo:** ✅ Archivo `test-happy-path-cartera.ps1` preparado en raíz del proyecto

---

**Generado:** 2026-03-29 13:15 UTC
**Ingeniero QA:** GitHub Copilot (Modo AIAgentExpert - QA Testing)
**Proyecto:** Plataforma Integral Fundación / Capítulo L.A.M.A. Medellín
