namespace VentusLibrary.WebApi.Swagger.Books;

/// <summary>
/// Metadatos de ejemplo para el esquema de BookDto usados por Swagger.
/// </summary>
public static class BookDtoSchema
{
    /// <summary>
    /// Construye metadatos de ejemplo para BookDto.
    /// </summary>
    /// <returns>Metadatos con valores de ejemplo.</returns>
    public static SchemaMetadata Build()
    {
        SchemaMetadata meta = new SchemaMetadata();
        meta.Example["id"] = 101;
        meta.Example["title"] = "Historia del Mundo Antiguo";
        meta.Example["description"] = "Resumen breve del contenido";
        meta.Example["publicationYear"] = -300;
        meta.Example["genreId"] = 5;
        meta.Example["authorId"] = 12;
        meta.Example["pageCount"] = 432;
        meta.Example["isSoftDelete"] = false;
        meta.Example["publicationYearText"] = "300 A.C";
        return meta;
    }
}
