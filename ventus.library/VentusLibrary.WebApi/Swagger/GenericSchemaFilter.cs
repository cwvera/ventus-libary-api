using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Application.Books.Commands;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Application.Genres.Dtos;
using VentusLibrary.WebApi.Swagger.Authors;
using VentusLibrary.WebApi.Swagger.Books;
using VentusLibrary.WebApi.Swagger.Genres;

namespace VentusLibrary.WebApi.Swagger;

/// <summary>
/// Filtro genérico de esquemas para enriquecer propiedades y ejemplos en Swagger
/// usando metadatos registrados por tipo.
/// </summary>
public class GenericSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Registro de tipos y factorías de metadatos para construir descripciones, rangos y ejemplos.
    /// </summary>
    private static readonly Dictionary<Type, Func<SchemaMetadata>> Registry = new()
    {
        { typeof(CreateBookCommand), CreateBookCommandSchema.Build },
        { typeof(UpdateBookCommand), UpdateBookCommandSchema.Build },
        { typeof(BookDto), BookDtoSchema.Build },
        { typeof(CreateAuthorCommand), CreateAuthorCommandSchema.Build },
        { typeof(UpdateAuthorCommand), UpdateAuthorCommandSchema.Build },
        { typeof(AuthorDto), AuthorDtoSchema.Build },
        { typeof(GenreDto), GenreDtoSchema.Build },
    };
    /// <summary>
    /// Mapea un valor de .NET a la representación adecuada de OpenAPI.
    /// </summary>
    private static IOpenApiAny ToOpenApiAny(object value) =>
       value switch
       {
           null => new OpenApiNull(),
           int i => new OpenApiInteger(i),
           long l => new OpenApiLong(l),
           bool b => new OpenApiBoolean(b),
           string s => new OpenApiString(s),
           DateTime dt => new OpenApiDate(dt),
           _ => new OpenApiString(value.ToString() ?? string.Empty)
       };

    /// <summary>
    /// Aplica descripciones, rangos y ejemplos al esquema actual según metadatos registrados.
    /// </summary>
    /// <param name="schema">Esquema OpenAPI de la operación/DTO actual.</param>
    /// <param name="context">Contexto del filtro con el tipo objetivo.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!Registry.TryGetValue(context.Type, out var factory)) return;
        SchemaMetadata meta = factory();

        foreach (KeyValuePair<string, string> kv in meta.Descriptions)
        {
            if (schema.Properties != null && schema.Properties.TryGetValue(kv.Key, out OpenApiSchema? prop))
            {
                prop.Description = kv.Value;
            }
        }

        foreach (KeyValuePair<string, RangeConstraint> kv in meta.NumericRanges)
        {
            if (schema.Properties != null && schema.Properties.TryGetValue(kv.Key, out OpenApiSchema? prop))
            {
                prop.Minimum = kv.Value.Minimum;
                prop.Maximum = kv.Value.Maximum;
            }
        }

        if (meta.Example.Count > 0)
        {
            OpenApiObject obj = new OpenApiObject();
            foreach (KeyValuePair<string, object> kv in meta.Example)
            {
                obj[kv.Key] = ToOpenApiAny(kv.Value);
            }
            schema.Example = obj;
        }
    }
}
