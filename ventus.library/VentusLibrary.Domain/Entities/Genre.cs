
namespace VentusLibrary.Domain.Entities;

/// <summary>
/// Representa un género literario dentro del catálogo.
/// </summary>
public class Genre
{
    /// <summary>Identificador único del género.</summary>
    public int Id { get; set; }
    /// <summary>Nombre del género.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Descripción opcional del género.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Indica si el registro está eliminado lógicamente (soft delete).</summary>
    public bool IsSoftDelete { get; set; }
    /// <summary>Fecha de creación del registro.</summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>Fecha de última actualización del registro.</summary>
    public DateTime? UpdatedAt { get; set; }
    /// <summary>Fecha de eliminación lógica del registro.</summary>
    public DateTime? DeletedAt { get; set; }
}
