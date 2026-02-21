using FluentValidation.Results;

namespace VentusLibrary.Application.Books.Services.BookLimits.Interfaces;

/// <summary>
/// Contrato para verificación de referencias de autor y género antes de operaciones de libro.
/// </summary>
public interface IBookReferencesChecker
{
    /// <summary>
    /// Recolecta errores si autor o género no existen o están soft-deleted.
    /// </summary>
    /// <param name="authorId">Id de autor.</param>
    /// <param name="genreId">Id de género.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Lista de fallos de validación.</returns>
    Task<IReadOnlyList<ValidationFailure>> CollectErrorsAsync(int authorId, int genreId, CancellationToken ct);
}
