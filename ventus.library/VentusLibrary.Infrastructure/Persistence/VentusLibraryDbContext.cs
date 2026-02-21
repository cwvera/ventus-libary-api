using Microsoft.EntityFrameworkCore;

namespace VentusLibrary.Infrastructure.Persistence;

/// <summary>
/// DbContext de la aplicación que mapea las entidades a las tablas relacionales.
/// </summary>
public class VentusLibraryDbContext : DbContext
{
    /// <summary>
    /// Inicializa el DbContext con las opciones configuradas por DI (cadena de conexión, proveedor, etc.).
    /// </summary>
    /// <param name="options">Opciones del DbContext (UseSqlServer, etc.).</param>
    public VentusLibraryDbContext(DbContextOptions<VentusLibraryDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Aplica todas las configuraciones de entidades encontradas en el ensamblado.
    /// </summary>
    /// <param name="modelBuilder">Constructor del modelo para registrar configuraciones.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VentusLibraryDbContext).Assembly);
    }
}
