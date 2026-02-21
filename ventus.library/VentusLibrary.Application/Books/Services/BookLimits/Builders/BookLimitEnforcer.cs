using FluentValidation;
using FluentValidation.Results;
using VentusLibrary.Application.Books.Services.BookLimits.Interfaces;

namespace VentusLibrary.Application.Books.Services.BookLimits.Builders;

/// <summary>
/// Orquestador que valida referencias y aplica reglas de límite antes de crear/actualizar libros.
/// </summary>
public class BookLimitEnforcer
{
    private readonly BookLimitRuleFactory _factory;
    private readonly IBookReferencesChecker _referencesChecker;

    /// <summary>
    /// Inicializa el enforcer con la factoría de reglas y el verificador de referencias.
    /// </summary>
    /// <param name="factory">Factoría de reglas de límites.</param>
    /// <param name="referencesChecker">Verificador de referencias de autor y género.</param>
    public BookLimitEnforcer(BookLimitRuleFactory factory, IBookReferencesChecker referencesChecker)
    {
        _factory = factory;
        _referencesChecker = referencesChecker;
    }

    /// <summary>
    /// Verifica que se pueda crear/actualizar un libro, lanzando ValidationException si hay infracciones.
    /// </summary>
    /// <param name="excludeBookId">Id de libro a excluir del conteo (para update).</param>
    /// <param name="authorId">Id de autor.</param>
    /// <param name="genreId">Id de género.</param>
    /// <param name="ct">Token de cancelación.</param>
    public async Task EnsureCanUpsertAsync(int? excludeBookId, int authorId, int genreId, CancellationToken ct)
    {
        List<ValidationFailure> failures = await CollectFailuresAsync(excludeBookId, authorId, genreId, ct);
        if (failures.Count > 0) throw new ValidationException(failures);
    }

    /// <summary>
    /// Recolecta errores de referencias y de reglas activas.
    /// </summary>
    /// <param name="excludeBookId">Id de libro a excluir del conteo.</param>
    /// <param name="authorId">Id de autor.</param>
    /// <param name="genreId">Id de género.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Lista de fallos de validación.</returns>
    private async Task<List<ValidationFailure>> CollectFailuresAsync(int? excludeBookId, int authorId, int genreId, CancellationToken ct)
    {
        List<ValidationFailure> failures = new List<ValidationFailure>();
        IReadOnlyList<ValidationFailure> referenceFailures = await _referencesChecker.CollectErrorsAsync(authorId, genreId, ct);
        failures.AddRange(referenceFailures);

        IReadOnlyList<IBookLimitRule> rules = await _factory.CreateActiveRulesAsync(ct);
        foreach (var rule in rules)
        {
            IReadOnlyList<ValidationFailure> ruleFailures = await rule.CheckUpsertAsync(excludeBookId, authorId, genreId, ct);
            failures.AddRange(ruleFailures);
        }
        return failures;
    }
}
