namespace VentusLibrary.WebApi.Swagger.Authors;

/// <summary>
/// Metadatos de ejemplo para el esquema de AuthorDto usados por Swagger.
/// </summary>
public static class AuthorDtoSchema
{
    /// <summary>
    /// Construye metadatos de ejemplo para AuthorDto.
    /// </summary>
    /// <returns>Metadatos con valores de ejemplo.</returns>
    public static SchemaMetadata Build()
    {
        SchemaMetadata meta = new SchemaMetadata();
        meta.Example["id"] = 12;
        meta.Example["fullName"] = "Jane Doe";
        meta.Example["description"] = "Autora de novela histórica";
        meta.Example["city"] = "Sevilla";
        meta.Example["email"] = "jane.doe@example.com";
        meta.Example["isSoftDelete"] = false;
        return meta;
    }
}
