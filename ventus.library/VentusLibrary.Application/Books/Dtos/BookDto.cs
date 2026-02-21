namespace VentusLibrary.Application.Books.Dtos;

/// <summary>
/// DTO para representar libros.
/// </summary>
public class BookDto
{
    /// <summary>Identificador único del libro.</summary>
    public int Id { get; set; }
    /// <summary>Título del libro.</summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>Descripción opcional.</summary>
    public string? Description { get; set; }
    /// <summary>Año de publicación.</summary>
    /// <remarks>Para era A.C use valores negativos. Ej.: -300 → 300 A.C.</remarks>
    public int? PublicationYear { get; set; }
    /// <summary>Formato del año (DC como número, AC como positivo + ' A.C').</summary>
    public string PublicationYearText
    {
        get
        {
            if (PublicationYear is null) return string.Empty;
            var y = PublicationYear.Value;
            return y >= 0 ? y.ToString() : $"{-y} A.C.";
        }
    }
    /// <summary>Identificador del género.</summary>
    public int GenreId { get; set; }
    /// <summary>Identificador del autor.</summary>
    public int AuthorId { get; set; }
    /// <summary>Cuenta de páginas.</summary>
    public int PageCount { get; set; }
    /// <summary>Indica si el registro está eliminado lógicamente.</summary>
    public bool IsSoftDelete { get; set; }
}
