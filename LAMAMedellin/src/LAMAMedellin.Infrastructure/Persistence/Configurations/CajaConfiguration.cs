using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAMAMedellin.Infrastructure.Persistence.Configurations;

public sealed class CajaConfiguration : IEntityTypeConfiguration<Caja>
{
    public void Configure(EntityTypeBuilder<Caja> builder)
    {
        builder.ToTable("Cajas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.TipoCaja)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.SaldoActual)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CuentaContableId)
            .IsRequired();

        builder.HasOne(x => x.CuentaContable)
            .WithMany()
            .HasForeignKey(x => x.CuentaContableId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CuentaContableId)
            .IsUnique();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
