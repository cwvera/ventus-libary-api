# Ventus Library API

## Resumen
- API .NET 8 para gestión de biblioteca con arquitectura por vertical slice.
- CQRS con MediatR, validación con FluentValidation, persistencia con EF Core 8 (SQL Server).
- Versionado de API (v1.0), Swagger habilitado.

## Stack
- .NET 8
- MediatR 14
- FluentValidation 12
- Entity Framework Core 8.0.8 (SqlServer, Design)
- Asp.Versioning

## Estructura
- VentusLibrary.Domain: entidades (Author, Genre, Book, BookLimit).
- VentusLibrary.Application: comandos/consultas/handlers/validators y servicios de negocio.
- VentusLibrary.Infrastructure: DbContext, configuraciones EF, DI de persistencia.
- VentusLibrary.WebApi: controladores, middleware, versionado y configuración.

## Configuración
- ConnectionStrings.VentusLibrary en appsettings.json:
  - Server=localhost;Database=VentusLibrary;Trusted_Connection=True;TrustServerCertificate=True
- Migrations creadas y aplicadas; usa DesignTimeDbContextFactory para soporte EF.

## Ejecución
- CLI: `dotnet run --project .\ventus.library\VentusLibrary.WebApi\VentusLibrary.WebApi.csproj`
- F5 en IDE: perfil “http/https” abre Swagger automáticamente.
- Swagger: `/swagger`
- Versión de API: `/api/v1.0/...`

## Endpoints Clave
- Genres (solo lectura):
  - GET `/api/genres`
  - GET `/api/genres/{id}`
- Books:
  - GET `/api/v1.0/books` → lista con AuthorName y GenreName
  - GET `/api/v1.0/books/{id}`
  - POST `/api/v1.0/books`
  - PUT `/api/v1.0/books/{id}`
  - DELETE `/api/v1.0/books/{id}`
- Authors:
  - GET `/api/authors`, GET `/api/authors/{id}`, POST, PUT, DELETE, Reactivate

## Políticas de Negocio
- Soft-delete: entidades marcan IsSoftDelete=true; Reactivate revierte.
- Unicidad:
  - Genre.Name único
  - Author.Email único
  - BookLimit única por LimitType
- BookLimits (enforcement):
  - Gestión manual desde BD (IsActive=1, IsSoftDelete=0)
  - Factory + Strategy:
    - Global: máximo de libros activos
    - Por Autor: máximo por AuthorId
    - Por Género: máximo por GenreId
  - Enforcer ejecuta reglas antes de crear el libro; errores lanzan ValidationException.

## Depuración
- Perfil http/https abre Swagger.
- Para disparar breakpoints en BooksController.ListAsync: invocar `GET /api/v1.0/books`.
- Compilar en Debug, reiniciar F5 si no engancha el breakpoint.

## Migraciones EF
- Actualizar BD:
  - `dotnet ef database update -p .\ventus.library\VentusLibrary.Infrastructure\VentusLibrary.Infrastructure.csproj -s .\ventus.library\VentusLibrary.Infrastructure\VentusLibrary.Infrastructure.csproj`
- Crear nueva migration:
  - `dotnet ef migrations add <Nombre> -p .\ventus.library\VentusLibrary.Infrastructure\VentusLibrary.Infrastructure.csproj -s .\ventus.library\VentusLibrary.Infrastructure\VentusLibrary.Infrastructure.csproj`

## Notas Operativas
- Si el build falla por archivos bloqueados, cerrar proceso WebApi/IDE en bin\Debug\net8.0.
- Asegurar .NET SDK 8 instalado; EF Core usa versión 8.0.8.
- BookLimits sin endpoints por ahora; se controlan desde la BD.

## Contribuciones
- Seguir patrones de vertical slice y CQRS.
- Mantener validaciones en FluentValidation y errores manejados por el middleware.
