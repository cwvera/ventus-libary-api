namespace VentusLibrary.Application.Books.Dtos;

/// <summary>
/// Item de listado de libros con detalles de autor y género para la grilla.
/// </summary>
public class BookListItemDto
{
    /// <summary>Identificador del libro.</summary>
    public int Id { get; set; }
    /// <summary>Título del libro.</summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>Identificador del autor.</summary>
    public int AuthorId { get; set; }
    /// <summary>Nombre completo del autor.</summary>
    public string AuthorName { get; set; } = string.Empty;
    /// <summary>Identificador del género.</summary>
    public int GenreId { get; set; }
    /// <summary>Nombre del género.</summary>
    public string GenreName { get; set; } = string.Empty;
    /// <summary>Año de publicación (opcional).</summary>
    public int? PublicationYear { get; set; }
    /// <summary>Cantidad de páginas (opcional).</summary>
    public int? PageCount { get; set; }
}
