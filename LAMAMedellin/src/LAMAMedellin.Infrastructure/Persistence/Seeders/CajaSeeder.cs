using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Seeders;

public static class CajaSeeder
{
    public static async Task SeedCajasAsync(this LamaDbContext context)
    {
        var cuentaCajaGeneral = await context.CuentasContables
            .FirstOrDefaultAsync(x => x.Codigo == "110505");

        if (cuentaCajaGeneral is null)
        {
            throw new InvalidOperationException("No existe la cuenta contable 110505 (Caja General) para sembrar cajas iniciales.");
        }

        var cuentaBancaria = await context.CuentasContables
            .FirstOrDefaultAsync(x => x.Codigo == "111005");

        if (cuentaBancaria is null)
        {
            throw new InvalidOperationException("No existe la cuenta contable 111005 (Moneda Nacional / Bancos) para sembrar cajas iniciales.");
        }

        if (!await context.Cajas.AnyAsync(x => x.Nombre == "Caja General L.A.M.A."))
        {
            await context.Cajas.AddAsync(new Caja(
                "Caja General L.A.M.A.",
                TipoCaja.Efectivo,
                0m,
                cuentaCajaGeneral.Id));
        }

        if (!await context.Cajas.AnyAsync(x => x.Nombre == "Cuenta Bancolombia"))
        {
            await context.Cajas.AddAsync(new Caja(
                "Cuenta Bancolombia",
                TipoCaja.Bancos,
                0m,
                cuentaBancaria.Id));
        }

        await context.SaveChangesAsync();
    }
}
