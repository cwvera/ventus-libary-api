namespace VentusLibrary.WebApi.Swagger.Authors;

/// <summary>
/// Metadatos para el esquema de UpdateAuthorCommand utilizados por Swagger.
/// </summary>
public static class UpdateAuthorCommandSchema
{
    /// <summary>
    /// Construye descripciones para UpdateAuthorCommand.
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
        return meta;
    }
}
