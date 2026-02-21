using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using VentusLibrary.Application;
using VentusLibrary.Infrastructure;
using VentusLibrary.WebApi.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    string xmlApp = Path.Combine(AppContext.BaseDirectory, "VentusLibrary.Application.xml");
    string xmlWeb = Path.Combine(AppContext.BaseDirectory, "VentusLibrary.WebApi.xml");
    if (File.Exists(xmlApp)) options.IncludeXmlComments(xmlApp, includeControllerXmlComments: true);
    if (File.Exists(xmlWeb)) options.IncludeXmlComments(xmlWeb, includeControllerXmlComments: true);
    options.SchemaFilter<VentusLibrary.WebApi.Swagger.GenericSchemaFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ventus Library API", Version = "v1" });
});

// Capas de aplicación e infraestructura
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Versionado de API
IApiVersioningBuilder apiVersioning = builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
apiVersioning.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
