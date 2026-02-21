
namespace VentusLibrary.Domain.Entities;

/// <summary>
/// Representa un libro dentro del sistema y su relación con autor y género.
/// </summary>
public class Book
{
    /// <summary>Identificador único del libro.</summary>
    public int Id { get; set; }
    /// <summary>Título del libro.</summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>Descripción opcional del libro.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Año de publicación (permite era AC mediante valores negativos).</summary>
    public int? PublicationYear { get; set; }
    /// <summary>Identificador del género.</summary>
    public int GenreId { get; set; }
    /// <summary>Número de páginas.</summary>
    public int PageCount { get; set; }
    /// <summary>Identificador del autor.</summary>
    public int AuthorId { get; set; }
    /// <summary>Indica si el registro está eliminado lógicamente (soft delete).</summary>
    public bool IsSoftDelete { get; set; }
    /// <summary>Fecha de creación del registro.</summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>Fecha de última actualización del registro.</summary>
    public DateTime? UpdatedAt { get; set; }
    /// <summary>Fecha de eliminación lógica del registro.</summary>
    public DateTime? DeletedAt { get; set; }
}
