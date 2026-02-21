using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Authors.Handlers;

/// <summary>
/// Handler para reactivar un autor previamente eliminado lógicamente.
/// </summary>
public class ReactivateAuthorHandler : IRequestHandler<ReactivateAuthorCommand, AuthorDto>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;

    public ReactivateAuthorHandler(VentusLibraryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<AuthorDto> Handle(ReactivateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Set<VentusLibrary.Domain.Entities.Author>().FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new InvalidOperationException("Autor no encontrado.");
        }

        entity.IsSoftDelete = false;
        entity.DeletedAt = null;
        entity.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AuthorDto>(entity);
    }
}
