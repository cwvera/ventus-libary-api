using VentusLibrary.Commons.Enums;

namespace VentusLibrary.Domain.Entities;

/// <summary>
/// Representa una regla de límite de creación de libros.
/// </summary>
public class BookLimit
{
    /// <summary>Identificador único de la regla.</summary>
    public int Id { get; set; }
    /// <summary>Tipo de límite.</summary>
    public BookLimitType LimitType { get; set; }
    /// <summary>Cantidad máxima de libros permitidos.</summary>
    public int MaxBooks { get; set; }
    /// <summary>Estado funcional de la regla.</summary>
    public bool IsActive { get; set; } = true;
    /// <summary>Indica si el registro está eliminado lógicamente (soft delete).</summary>
    public bool IsSoftDelete { get; set; }
    /// <summary>Fecha de creación del registro.</summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>Fecha de última actualización del registro.</summary>
    public DateTime? UpdatedAt { get; set; }
    /// <summary>Fecha de eliminación lógica del registro.</summary>
    public DateTime? DeletedAt { get; set; }
}
