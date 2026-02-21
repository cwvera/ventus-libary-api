using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de EF Core para la entidad Book: tabla, relaciones e índices.
/// </summary>
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    /// <summary>
    /// Configura el mapeo de Book a la base de datos, incluyendo relaciones con Genre y Author.
    /// </summary>
    /// <param name="entity">Constructor de la entidad para definir propiedades y claves foráneas.</param>
    public void Configure(EntityTypeBuilder<Book> entity)
    {
        entity.ToTable("Book", "dbo");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
        entity.Property(x => x.Description).HasMaxLength(200);
        entity.Property(x => x.PublicationYear);
        entity.Property(x => x.PageCount).IsRequired();

        entity.HasOne<Genre>()
              .WithMany()
              .HasForeignKey(x => x.GenreId)
              .OnDelete(DeleteBehavior.NoAction);

        entity.HasOne<Author>()
              .WithMany()
              .HasForeignKey(x => x.AuthorId)
              .OnDelete(DeleteBehavior.NoAction);

        entity.HasIndex(x => x.AuthorId);
        entity.HasIndex(x => x.GenreId);
    }
}
