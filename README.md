VENTUS LIBRARY API

Backend del sistema Ventus Library, desarrollado en .NET 8. Permite la
gestión de autores, géneros y libros con validación dinámica de reglas
de negocio configurables.

  ------------------------------------------------------------
  TECNOLOGÍAS UTILIZADAS
  ------------------------------------------------------------
  - .NET 8 - ASP.NET Core Web API - Entity Framework Core 8 -
  SQL Server - CQRS - FluentValidation - Swagger - Dependency
  Injection

  ------------------------------------------------------------

ARQUITECTURA

Domain Application Infrastructure WebApi ventus.library (Script SQL)

Descripción de capas:

-   Domain: Entidades y reglas de negocio.
-   Application: Casos de uso (Commands / Queries), validaciones y
    lógica orquestadora.
-   Infrastructure: DbContext y configuración de acceso a datos.
-   WebApi: Controllers y configuración HTTP.

Patrones implementados: - CQRS - Strategy Pattern (reglas de límite) -
Factory Pattern (resolución dinámica de reglas activas) - Middleware
global de excepciones

  --------------------------------
  CONFIGURACIÓN DE BASE DE DATOS
  --------------------------------

La base de datos se crea mediante el script:

ventus.library/VentusLibrary.sql

Pasos:

1.  Abrir SQL Server.
2.  Ejecutar el archivo VentusLibrary.sql.
3.  Verificar que la base de datos se haya creado.
4.  Configurar la cadena de conexión en appsettings.json.

El proyecto NO utiliza migraciones automáticas.

  ------------------------
  EJECUCIÓN DEL PROYECTO
  ------------------------

Desde la raíz del proyecto ejecutar:

dotnet restore dotnet run

Swagger estará disponible en:

https://localhost:{puerto}/swagger

  -------------------
  REGLAS DE NEGOCIO
  -------------------

-   El autor debe existir antes de registrar un libro.
-   Se valida el límite máximo de libros según reglas activas:
    -   Global
    -   Por Autor
    -   Por Género

Si se supera el límite permitido: “No es posible registrar el libro, se
alcanzó el máximo permitido.”

  -------
  NOTAS
  -------

-   No se implementa autenticación ni autorización (fuera del alcance).
-   No se implementan políticas de reintento.
-   Se aplica Soft Delete en entidades principales.
-   Manejo centralizado de excepciones.
