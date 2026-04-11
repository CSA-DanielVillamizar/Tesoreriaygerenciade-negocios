import { z } from 'zod';

const tipoMiembroValidos = [1, 2, 3, 4] as const;

export const crearMiembroSchema = z.object({
    documentoIdentidad: z
        .string()
        .trim()
        .min(1, 'Documento de identidad es obligatorio.')
        .max(30, 'Documento de identidad no puede exceder 30 caracteres.'),
    nombres: z
        .string()
        .trim()
        .min(1, 'Nombres es obligatorio.')
        .max(100, 'Nombres no puede exceder 100 caracteres.'),
    apellidos: z
        .string()
        .trim()
        .min(1, 'Apellidos es obligatorio.')
        .max(100, 'Apellidos no puede exceder 100 caracteres.'),
    apodo: z
        .string()
        .trim()
        .max(120, 'Apodo no puede exceder 120 caracteres.'),
    fechaIngreso: z
        .string()
        .trim()
        .min(1, 'Fecha de ingreso es obligatoria.')
        .refine((value) => !Number.isNaN(new Date(value).getTime()), 'Fecha de ingreso inválida.')
        .refine((value) => {
            const fecha = new Date(`${value}T00:00:00`);
            const hoy = new Date();
            hoy.setHours(0, 0, 0, 0);
            return fecha <= hoy;
        }, 'Fecha de ingreso no puede ser mayor a hoy.'),
    tipoMiembro: z
        .coerce
        .number()
        .int('Tipo de miembro inválido.')
        .refine((value) => tipoMiembroValidos.includes(value as (typeof tipoMiembroValidos)[number]), 'Tipo de miembro debe ser un valor válido.'),
});

export type CrearMiembroFormInput = z.input<typeof crearMiembroSchema>;
export type CrearMiembroFormValues = z.output<typeof crearMiembroSchema>;

// ---------------------------------------------------------------------------
// Crear Concepto de Cobro
// Mirrors: CrearConceptoCobroCommandValidator.cs
// ---------------------------------------------------------------------------
const uuidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;

export const crearConceptoCobroSchema = z.object({
    nombre: z
        .string()
        .trim()
        .min(1, 'Nombre es obligatorio.')
        .max(150, 'Nombre no puede exceder 150 caracteres.'),
    valorCOP: z
        .coerce
        .number({ error: 'ValorCOP debe ser un número.' })
        .gt(0, 'ValorCOP debe ser mayor a cero.'),
    periodicidadMensual: z
        .coerce
        .number({ error: 'Periodicidad debe ser un número.' })
        .int('Periodicidad debe ser un número entero.')
        .gt(0, 'PeriodicidadMensual debe ser mayor a cero.'),
    cuentaContableIngresoId: z
        .string()
        .trim()
        .min(1, 'CuentaContableIngresoId es obligatorio.')
        .regex(uuidRegex, 'CuentaContableIngresoId debe tener formato UUID (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).'),
});

export type CrearConceptoCobroFormInput = z.input<typeof crearConceptoCobroSchema>;
export type CrearConceptoCobroFormValues = z.output<typeof crearConceptoCobroSchema>;

// ---------------------------------------------------------------------------
// Crear Cuenta por Cobrar
// Mirrors: CrearCuentaPorCobrarCommandValidator.cs
// ---------------------------------------------------------------------------
export const crearCuentaPorCobrarSchema = z
    .object({
        miembroId: z
            .string()
            .trim()
            .min(1, 'MiembroId es obligatorio.')
            .regex(uuidRegex, 'MiembroId debe tener formato UUID (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).'),
        conceptoCobroId: z
            .string()
            .trim()
            .min(1, 'ConceptoCobroId es obligatorio.')
            .regex(uuidRegex, 'ConceptoCobroId debe tener formato UUID (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).'),
        fechaEmision: z
            .string()
            .trim()
            .min(1, 'FechaEmision es obligatoria.')
            .refine((v) => !Number.isNaN(new Date(v).getTime()), 'FechaEmision inválida.'),
        fechaVencimiento: z
            .string()
            .trim()
            .min(1, 'FechaVencimiento es obligatoria.')
            .refine((v) => !Number.isNaN(new Date(v).getTime()), 'FechaVencimiento inválida.'),
        valorTotal: z
            .coerce
            .number({ error: 'ValorTotal debe ser un número.' })
            .gt(0, 'ValorTotal debe ser mayor a cero.'),
    })
    .superRefine((data, ctx) => {
        if (
            data.fechaEmision &&
            data.fechaVencimiento &&
            !Number.isNaN(new Date(data.fechaEmision).getTime()) &&
            !Number.isNaN(new Date(data.fechaVencimiento).getTime()) &&
            new Date(`${data.fechaVencimiento}T00:00:00`) < new Date(`${data.fechaEmision}T00:00:00`)
        ) {
            ctx.addIssue({
                code: z.ZodIssueCode.custom,
                path: ['fechaVencimiento'],
                message: 'FechaVencimiento debe ser mayor o igual a FechaEmision.',
            });
        }
    });

export type CrearCuentaPorCobrarFormInput = z.input<typeof crearCuentaPorCobrarSchema>;
export type CrearCuentaPorCobrarFormValues = z.output<typeof crearCuentaPorCobrarSchema>;

// ---------------------------------------------------------------------------
// Registrar Pago Cartera
// Mirrors: RegistrarPagoCarteraCommandValidator.cs
// ---------------------------------------------------------------------------
export const registrarPagoCarteraSchema = z.object({
    monto: z
        .coerce
        .number({ error: 'Monto debe ser un número.' })
        .gt(0, 'Monto debe ser mayor a cero.'),
});

export type RegistrarPagoCarteraFormInput = z.input<typeof registrarPagoCarteraSchema>;
export type RegistrarPagoCarteraFormValues = z.output<typeof registrarPagoCarteraSchema>;
