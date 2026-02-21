namespace VentusLibrary.Application.Authors.Dtos;

/// <summary>
/// DTO para representar autores en respuestas de la API.
/// </summary>
public class AuthorDto
{
    /// <summary>Identificador único del autor.</summary>
    public int Id { get; set; }
    /// <summary>Nombre completo del autor.</summary>
    public string FullName { get; set; } = string.Empty;
    /// <summary>Descripción opcional del autor.</summary>
    public string? Description { get; set; }
    /// <summary>Fecha de nacimiento.</summary>
    public DateTime? BirthDate { get; set; }
    /// <summary>Ciudad de procedencia.</summary>
    public string? City { get; set; }
    /// <summary>Correo electrónico.</summary>
    public string? Email { get; set; }
    /// <summary>Indica si el registro está eliminado lógicamente.</summary>
    public bool IsSoftDelete { get; set; }
}
