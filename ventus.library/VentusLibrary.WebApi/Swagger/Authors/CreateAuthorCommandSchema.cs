namespace VentusLibrary.WebApi.Swagger.Authors;

/// <summary>
/// Metadatos para el esquema de CreateAuthorCommand utilizados por Swagger.
/// </summary>
public static class CreateAuthorCommandSchema
{
    /// <summary>
    /// Construye descripciones y ejemplo para CreateAuthorCommand.
    /// </summary>
    /// <returns>Metadatos de esquema.</returns>
    public static SchemaMetadata Build()
    {
        SchemaMetadata meta = new SchemaMetadata();
        meta.Descriptions["fullName"] = "Nombre completo del autor\n- Requerido\n- Máx: 200 caracteres";
        meta.Descriptions["description"] = "Descripción del autor\n- Opcional\n- Máx: 200 caracteres";
        meta.Descriptions["birthDate"] = "Fecha de nacimiento\n- Opcional\n- Formato ISO 8601 (YYYY-MM-DD)";
        meta.Descriptions["city"] = "Ciudad\n- Opcional\n- Máx: 100 caracteres";
        meta.Descriptions["email"] = "Correo electrónico\n- Opcional\n- Formato válido de email";
        meta.Example["fullName"] = "Jane Doe";
        meta.Example["description"] = "Autora de novela histórica";
        meta.Example["birthDate"] = new DateTime(1990, 5, 12);
        meta.Example["city"] = "Sevilla";
        meta.Example["email"] = "jane.doe@example.com";
        return meta;
    }
}
