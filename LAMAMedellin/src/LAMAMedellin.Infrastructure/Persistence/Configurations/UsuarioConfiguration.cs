using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAMAMedellin.Infrastructure.Persistence.Configurations;

public sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.EntraObjectId)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(u => u.EntraObjectId)
            .IsUnique();

        builder.Property(u => u.Rol)
            .HasConversion<int>()
            .HasDefaultValue(RolSistema.CapitanRuta)
            .IsRequired();

        builder.Property(u => u.EsActivo)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(u => u.MiembroId)
            .IsRequired(false);

        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}
