namespace VentusLibrary.WebApi.Swagger.Genres;

/// <summary>
/// Metadatos de ejemplo para el esquema de GenreDto usados por Swagger.
/// </summary>
public static class GenreDtoSchema
{
    /// <summary>
    /// Construye metadatos de ejemplo para GenreDto.
    /// </summary>
    /// <returns>Metadatos con valores de ejemplo.</returns>
    public static SchemaMetadata Build()
    {
        SchemaMetadata meta = new();
        meta.Example["id"] = 5;
        meta.Example["name"] = "Historia";
        meta.Example["description"] = "Libros de historia y crónica";
        meta.Example["isSoftDelete"] = false;
        return meta;
    }
}
