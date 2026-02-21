namespace VentusLibrary.WebApi.Swagger;

/// <summary>
/// Restricción numérica para propiedades: mínimo y máximo opcionales.
/// </summary>
public class RangeConstraint
{
    /// <summary>Valor mínimo permitido (inclusive).</summary>
    public int? Minimum { get; set; }
    /// <summary>Valor máximo permitido (inclusive).</summary>
    public int? Maximum { get; set; }
}

/// <summary>
/// Metadatos auxiliares para enriquecer esquemas Swagger (descripciones, rangos numéricos y ejemplos).
/// </summary>
public class SchemaMetadata
{
    /// <summary>Descripciones por nombre de propiedad.</summary>
    public Dictionary<string, string> Descriptions { get; } = new();
    /// <summary>Rangos numéricos por propiedad.</summary>
    public Dictionary<string, RangeConstraint> NumericRanges { get; } = new();
    /// <summary>Ejemplo de objeto para el esquema.</summary>
    public Dictionary<string, object> Example { get; } = new();
}
