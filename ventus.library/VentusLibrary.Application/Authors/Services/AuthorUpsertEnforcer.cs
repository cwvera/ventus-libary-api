using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Authors.Services;

public class AuthorUpsertEnforcer
{
    private readonly VentusLibraryDbContext _db;

    public AuthorUpsertEnforcer(VentusLibraryDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Valida si se puede crear un autor con el email dado. Si existe activo, lanza ValidationException.
    /// </summary>
    public async Task EnsureCanCreateAsync(Author? existing, CancellationToken ct)
    {
        var failures = new List<ValidationFailure>();
        if (existing is { IsSoftDelete: false })
            failures.Add(new ValidationFailure("Email", "Ya existe un autor activo con el mismo correo electrónico."));

        if (failures.Count > 0) throw new ValidationException(failures);
    }

    /// <summary>
    /// Valida si se puede actualizar el autor con email nuevo. Si otro activo usa el mismo email, lanza ValidationException.
    /// </summary>
    public async Task EnsureCanUpdateAsync(int id, string? email, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email)) return;
        var other = await _db.Set<Author>()
            .FirstOrDefaultAsync(a => a.Email == email && a.Id != id, ct);
        if (other is { IsSoftDelete: false })
            throw new ValidationException([new ValidationFailure("Email", "Otro autor activo ya usa el mismo correo electrónico.")]);
    }
}
