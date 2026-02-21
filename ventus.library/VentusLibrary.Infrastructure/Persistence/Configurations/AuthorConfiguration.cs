using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de EF Core para la entidad Author: tabla, claves, longitudes e índices.
/// </summary>
public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    /// <summary>
    /// Configura el mapeo de Author a la base de datos.
    /// </summary>
    /// <param name="entity">Constructor de la entidad para definir propiedades y restricciones.</param>
    public void Configure(EntityTypeBuilder<Author> entity)
    {
        entity.ToTable("Author", "dbo");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
        entity.Property(x => x.Description).HasMaxLength(200);
        entity.Property(x => x.City).HasMaxLength(100);
        entity.Property(x => x.Email).HasMaxLength(100);
        entity.HasIndex(x => x.Email).IsUnique();
    }
}
