using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Commands;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Handlers;

/// <summary>
/// Handler para realizar el borrado lógico de un libro.
/// </summary>
public class SoftDeleteBookHandler : IRequestHandler<SoftDeleteBookCommand, bool>
{
    private readonly VentusLibraryDbContext _db;

    public SoftDeleteBookHandler(VentusLibraryDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(SoftDeleteBookCommand request, CancellationToken cancellationToken)
    {
        Book? entity = await _db.Set<Book>().FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (entity == null || entity.IsSoftDelete) return false;
        entity.IsSoftDelete = true;
        entity.DeletedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
