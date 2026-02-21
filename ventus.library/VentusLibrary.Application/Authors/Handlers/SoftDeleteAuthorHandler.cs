using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Authors.Handlers;

/// <summary>
/// Handler para realizar el borrado lógico de un autor.
/// </summary>
public class SoftDeleteAuthorHandler : IRequestHandler<SoftDeleteAuthorCommand, bool>
{
    private readonly VentusLibraryDbContext _db;

    public SoftDeleteAuthorHandler(VentusLibraryDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(SoftDeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Set<VentusLibrary.Domain.Entities.Author>().FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (entity == null || entity.IsSoftDelete)
        {
            return false;
        }

        entity.IsSoftDelete = true;
        entity.DeletedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
