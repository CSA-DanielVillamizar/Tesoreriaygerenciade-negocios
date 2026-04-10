using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAMAMedellin.Infrastructure.Persistence.Configurations;

public sealed class ConceptoCobroConfiguration : IEntityTypeConfiguration<ConceptoCobro>
{
    public void Configure(EntityTypeBuilder<ConceptoCobro> builder)
    {
        builder.ToTable("ConceptosCobro");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.ValorCOP)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.PeriodicidadMensual)
            .IsRequired();

        builder.Property(c => c.CuentaContableIngresoId)
            .IsRequired();

        builder.HasIndex(c => c.Nombre)
            .IsUnique();

        builder.HasOne<CuentaContable>()
            .WithMany()
            .HasForeignKey(c => c.CuentaContableIngresoId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}
