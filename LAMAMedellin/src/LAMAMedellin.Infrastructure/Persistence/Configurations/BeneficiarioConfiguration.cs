using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAMAMedellin.Infrastructure.Persistence.Configurations;

public sealed class BeneficiarioConfiguration : IEntityTypeConfiguration<Beneficiario>
{
    public void Configure(EntityTypeBuilder<Beneficiario> builder)
    {
        builder.ToTable("Beneficiarios");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.ProyectoSocialId)
            .IsRequired();

        builder.Property(b => b.NombreCompleto)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.TipoDocumento)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(b => b.NumeroDocumento)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(b => b.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Telefono)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(b => b.TieneConsentimientoHabeasData)
            .IsRequired();

        builder.HasIndex(b => new { b.TipoDocumento, b.NumeroDocumento })
            .IsUnique();

        builder.HasIndex(b => b.ProyectoSocialId);

        builder.HasOne(b => b.ProyectoSocial)
            .WithMany()
            .HasForeignKey(b => b.ProyectoSocialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}
