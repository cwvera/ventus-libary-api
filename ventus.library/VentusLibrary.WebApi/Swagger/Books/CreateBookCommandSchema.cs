namespace VentusLibrary.WebApi.Swagger.Books;

/// <summary>
/// Metadatos para el esquema de CreateBookCommand utilizados por Swagger.
/// </summary>
public static class CreateBookCommandSchema
{
    /// <summary>
    /// Construye descripciones, rangos numéricos y ejemplo para CreateBookCommand.
    /// </summary>
    /// <returns>Metadatos de esquema.</returns>
    public static SchemaMetadata Build()
    {
        SchemaMetadata meta = new SchemaMetadata();
        meta.Descriptions["title"] = "Título del libro\n- Requerido\n- Máx: 200 caracteres";
        meta.Descriptions["description"] = "Descripción del libro\n- Opcional\n- Máx: 200 caracteres";
        meta.Descriptions["publicationYear"] = "Año de publicación\n- DC: año positivo o 0\n- AC: enviar negativo (ej.: -300 → 300 A.C)\n- Rango: [-3000, año actual]";
        meta.Descriptions["genreId"] = "Identificador de género\n- Entero > 0";
        meta.Descriptions["authorId"] = "Identificador de autor\n- Entero > 0";
        meta.Descriptions["pageCount"] = "Número de páginas\n- Entero > 0";
        meta.NumericRanges["publicationYear"] = new RangeConstraint { Minimum = -3000, Maximum = DateTime.UtcNow.Year };
        meta.Example["title"] = "Historia del Mundo Antiguo";
        meta.Example["description"] = "Resumen breve del contenido";
        meta.Example["publicationYear"] = -300;
        meta.Example["genreId"] = 5;
        meta.Example["authorId"] = 12;
        meta.Example["pageCount"] = 432;
        return meta;
    }
}
