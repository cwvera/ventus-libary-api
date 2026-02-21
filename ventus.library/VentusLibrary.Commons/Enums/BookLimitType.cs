namespace VentusLibrary.Commons.Enums;

/// <summary>
/// Tipos de límites aplicables a la creación/gestión de libros.
/// </summary>
public enum BookLimitType : byte
{
    /// <summary>Límite global aplicable a todo el catálogo.</summary>
    Global = 1,
    /// <summary>Límite específico por autor.</summary>
    Author = 2,
    /// <summary>Límite específico por género.</summary>
    Genre = 3
}
