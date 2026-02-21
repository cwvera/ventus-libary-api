using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de EF Core para la entidad Genre: tabla, propiedades y restricciones.
/// </summary>
public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    /// <summary>
    /// Configura el mapeo de Genre a la base de datos.
    /// </summary>
    /// <param name="entity">Constructor de la entidad para definir propiedades y restricciones.</param>
    public void Configure(EntityTypeBuilder<Genre> entity)
    {
        entity.ToTable("Genre", "dbo");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
        entity.Property(x => x.Description).HasMaxLength(200);
    }
}
