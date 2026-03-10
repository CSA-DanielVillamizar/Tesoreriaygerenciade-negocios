using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAMAMedellin.Infrastructure.Persistence.Configurations;

public sealed class TarifaCuotaConfiguration : IEntityTypeConfiguration<TarifaCuota>
{
    public void Configure(EntityTypeBuilder<TarifaCuota> builder)
    {
        builder.ToTable("TarifasCuota");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TipoAfiliacion)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ValorMensualCOP)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasIndex(x => x.TipoAfiliacion)
            .IsUnique();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
