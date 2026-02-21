using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de EF Core para la entidad BookLimit: tabla, enumeraciones e índices.
/// </summary>
public class BookLimitConfiguration : IEntityTypeConfiguration<BookLimit>
{
    /// <summary>
    /// Configura el mapeo de BookLimit, incluyendo conversión de enum a byte y restricciones.
    /// </summary>
    /// <param name="entity">Constructor de la entidad para definir propiedades e índices.</param>
    public void Configure(EntityTypeBuilder<BookLimit> entity)
    {
        entity.ToTable("BookLimit", "dbo");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.LimitType)
              .HasConversion<byte>()
              .IsRequired();
        entity.Property(x => x.MaxBooks).IsRequired();
        entity.Property(x => x.IsActive).IsRequired();
        entity.HasIndex(x => x.LimitType).IsUnique();
    }
}
