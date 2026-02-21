using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Services.BookLimits.Interfaces;
using VentusLibrary.Application.Books.Services.BookLimits.Rules;
using VentusLibrary.Commons.Enums;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Services.BookLimits.Builders;

/// <summary>
/// Crea reglas de límites de libros activas a partir de la configuración persistida.
/// </summary>
public class BookLimitRuleFactory
{
    private readonly VentusLibraryDbContext _db;

    /// <summary>
    /// Inicializa la factoría con acceso al DbContext.
    /// </summary>
    /// <param name="db">Contexto de base de datos.</param>
    public BookLimitRuleFactory(VentusLibraryDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Construye la lista de reglas activas según los registros BookLimit.
    /// </summary>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Listado de reglas de límite activas.</returns>
    public async Task<IReadOnlyList<IBookLimitRule>> CreateActiveRulesAsync(CancellationToken ct)
    {
        List<BookLimit> limits = await _db.Set<BookLimit>()
            .Where(x => x.IsActive && !x.IsSoftDelete)
            .ToListAsync(ct);

        List<IBookLimitRule> rules = new();
        foreach (BookLimit limit in limits)
        {
            switch (limit.LimitType)
            {
                case BookLimitType.Global:
                    rules.Add(new GlobalBookLimitRule(_db, limit.MaxBooks));
                    break;
                case BookLimitType.Author:
                    rules.Add(new AuthorBookLimitRule(_db, limit.MaxBooks));
                    break;
                case BookLimitType.Genre:
                    rules.Add(new GenreBookLimitRule(_db, limit.MaxBooks));
                    break;
            }
        }
        return rules;
    }
}
