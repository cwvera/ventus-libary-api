using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Services.BookLimits.Interfaces;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Services.BookLimits.Rules;

/// <summary>
/// Regla que limita la cantidad de libros por autor.
/// </summary>
public class AuthorBookLimitRule : IBookLimitRule
{
    private readonly VentusLibraryDbContext _db;
    private readonly int _max;

    /// <summary>
    /// Inicializa la regla con el máximo permitido.
    /// </summary>
    /// <param name="db">DbContext.</param>
    /// <param name="max">Cantidad máxima de libros por autor.</param>
    public AuthorBookLimitRule(VentusLibraryDbContext db, int max)
    {
        _db = db;
        _max = max;
    }

    /// <summary>
    /// Verifica la regla para una combinación de autor/género.
    /// </summary>
    public async Task<IReadOnlyList<ValidationFailure>> CheckAsync(int authorId, int genreId, CancellationToken ct) =>
        await CheckUpsertAsync(null, authorId, genreId, ct);

    /// <summary>
    /// Verifica la regla excluyendo un libro (update).
    /// </summary>
    public async Task<IReadOnlyList<ValidationFailure>> CheckUpsertAsync(int? excludeBookId, int authorId, int genreId, CancellationToken ct)
    {
        int count = await _db.Set<Book>().CountAsync(
            b => !b.IsSoftDelete && b.AuthorId == authorId && (excludeBookId == null || b.Id != excludeBookId),
            ct);
        if (count > _max) return [new ValidationFailure("AuthorLimit", "Se alcanzó el límite de libros por autor.")];
        return [];
    }
}
