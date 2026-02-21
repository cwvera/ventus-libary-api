
namespace VentusLibrary.Domain.Entities;

/// <summary>
/// Representa un autor de obras literarias dentro del sistema.
/// </summary>
public class Author
{
    /// <summary>Identificador único del autor.</summary>
    public int Id { get; set; }
    /// <summary>Nombre completo del autor.</summary>
    public string FullName { get; set; } = string.Empty;
    /// <summary>Descripción opcional del autor.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Fecha de nacimiento (puede ser NULL si es desconocida).</summary>
    public DateTime? BirthDate { get; set; }
    /// <summary>Ciudad de procedencia (puede ser NULL si se desconoce).</summary>
    public string City { get; set; } = string.Empty;
    /// <summary>Correo electrónico (opcional).</summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>Indica si el registro está eliminado lógicamente (soft delete).</summary>
    public bool IsSoftDelete { get; set; }
    /// <summary>Fecha de creación del registro.</summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>Fecha de última actualización del registro.</summary>
    public DateTime? UpdatedAt { get; set; }
    /// <summary>Fecha de eliminación lógica del registro.</summary>
    public DateTime? DeletedAt { get; set; }
}
