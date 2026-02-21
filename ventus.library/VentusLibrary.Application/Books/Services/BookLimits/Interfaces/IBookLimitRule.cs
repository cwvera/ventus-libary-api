using FluentValidation.Results;

namespace VentusLibrary.Application.Books.Services.BookLimits.Interfaces;

/// <summary>
/// Contrato para reglas de límites de libros (global, por autor, por género).
/// </summary>
public interface IBookLimitRule
{
    /// <summary>
    /// Verifica la regla para una combinación de autor/género.
    /// </summary>
    /// <param name="authorId">Id de autor.</param>
    /// <param name="genreId">Id de género.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Lista de fallos si la regla se incumple.</returns>
    Task<IReadOnlyList<ValidationFailure>> CheckAsync(int authorId, int genreId, CancellationToken ct);
    /// <summary>
    /// Verifica la regla permitiendo excluir un libro (para update).
    /// </summary>
    /// <param name="excludeBookId">Id de libro a excluir del conteo.</param>
    /// <param name="authorId">Id de autor.</param>
    /// <param name="genreId">Id de género.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Lista de fallos si la regla se incumple.</returns>
    Task<IReadOnlyList<ValidationFailure>> CheckUpsertAsync(int? excludeBookId, int authorId, int genreId, CancellationToken ct);
}
