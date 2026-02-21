using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Services.BookLimits.Interfaces;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Services.BookLimits.Builders;

/// <summary>
/// Verifica que autor y género referenciados existan y estén activos antes del upsert.
/// </summary>
public class BookReferencesChecker : IBookReferencesChecker
{
    private readonly VentusLibraryDbContext _db;

    /// <summary>
    /// Inicializa el verificador con el DbContext.
    /// </summary>
    /// <param name="db">Contexto de base de datos.</param>
    public BookReferencesChecker(VentusLibraryDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Recolecta errores de referencias faltantes para autor y género.
    /// </summary>
    /// <param name="authorId">Id de autor.</param>
    /// <param name="genreId">Id de género.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Lista de fallos si la referencia no existe o está eliminada.</returns>
    public async Task<IReadOnlyList<ValidationFailure>> CollectErrorsAsync(int authorId, int genreId, CancellationToken ct)
    {
        var markers = await _db.Set<Author>()
            .Where(a => a.Id == authorId && !a.IsSoftDelete)
            .Select(_ => new { A = true, G = false })
            .Concat(
                _db.Set<Genre>()
                   .Where(g => g.Id == genreId && !g.IsSoftDelete)
                   .Select(_ => new { A = false, G = true })
            )
            .ToListAsync(ct);

        List<ValidationFailure> failures = new List<ValidationFailure>(2);
        bool authorOk = markers.Any(m => m.A);
        bool genreOk = markers.Any(m => m.G);

        if (!authorOk) failures.Add(new ValidationFailure("AuthorId", "El autor no está registrado."));
        if (!genreOk) failures.Add(new ValidationFailure("GenreId", "El autor no está registrado."));

        return failures;
    }
}
