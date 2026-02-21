using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Infrastructure;

/// <summary>
/// Registro de dependencias de la capa de infraestructura.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Agrega servicios de infraestructura: DbContext y conexiones.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("VentusLibrary");

        services.AddDbContext<VentusLibraryDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
