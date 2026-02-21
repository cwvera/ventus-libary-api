using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Services.BookLimits.Interfaces;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Services.BookLimits.Rules;

/// <summary>
/// Regla que limita la cantidad total de libros del catálogo.
/// </summary>
public class GlobalBookLimitRule : IBookLimitRule
{
    private readonly VentusLibraryDbContext _db;
    private readonly int _max;

    /// <summary>
    /// Inicializa la regla con el máximo global permitido.
    /// </summary>
    /// <param name="db">DbContext.</param>
    /// <param name="max">Cantidad máxima total de libros.</param>
    public GlobalBookLimitRule(VentusLibraryDbContext db, int max)
    {
        _db = db;
        _max = max;
    }

    /// <summary>
    /// Verifica la regla para autor/género (no condiciona, es global).
    /// </summary>
    public async Task<IReadOnlyList<ValidationFailure>> CheckAsync(int authorId, int genreId, CancellationToken ct) =>
        await CheckUpsertAsync(null, authorId, genreId, ct);

    /// <summary>
    /// Verifica la regla excluyendo un libro (update).
    /// </summary>
    public async Task<IReadOnlyList<ValidationFailure>> CheckUpsertAsync(int? excludeBookId, int authorId, int genreId, CancellationToken ct)
    {
        int count = await _db.Set<Domain.Entities.Book>().CountAsync(
            b => !b.IsSoftDelete && (excludeBookId == null || b.Id != excludeBookId),
            ct);
        if (count > _max) return [new ValidationFailure("GlobalLimit", "Se alcanzó el límite global de libros.")];
        return [];
    }
}
