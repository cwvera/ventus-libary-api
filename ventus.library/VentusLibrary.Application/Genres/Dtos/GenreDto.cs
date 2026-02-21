namespace VentusLibrary.Application.Genres.Dtos;

/// <summary>
/// DTO para representar géneros literarios.
/// </summary>
public class GenreDto
{
    /// <summary>Identificador único del género.</summary>
    public int Id { get; set; }
    /// <summary>Nombre del género.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Descripción opcional del género.</summary>
    public string? Description { get; set; }
    /// <summary>Indica si el registro está eliminado lógicamente.</summary>
    public bool IsSoftDelete { get; set; }
}
