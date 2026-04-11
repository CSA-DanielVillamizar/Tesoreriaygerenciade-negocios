using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAMAMedellin.Infrastructure.Persistence.Configurations;

public sealed class CuentaPorCobrarConfiguration : IEntityTypeConfiguration<CuentaPorCobrar>
{
    public void Configure(EntityTypeBuilder<CuentaPorCobrar> builder)
    {
        builder.ToTable("CuentasPorCobrar");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.MiembroId)
            .IsRequired();

        builder.Property(c => c.ConceptoCobroId)
            .HasDefaultValue(Guid.Empty)
            .IsRequired();

        builder.Property(c => c.FechaEmision)
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(c => c.FechaVencimiento)
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(c => c.ValorTotal)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(c => c.SaldoPendiente)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(c => c.Estado)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(c => c.Miembro)
            .WithMany()
            .HasForeignKey(c => c.MiembroId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(c => c.ConceptoCobro)
            .WithMany()
            .HasForeignKey(c => c.ConceptoCobroId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasIndex(c => new { c.MiembroId, c.ConceptoCobroId, c.FechaEmision })
            .IsUnique()
            .HasName("IX_CuentasPorCobrar_MiembroConceptoFecha");

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}
