using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Application.Authors.Services;
using VentusLibrary.Commons.Messages;
using VentusLibrary.Commons.Responses;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Authors.Handlers;

/// <summary>
/// Handler para crear un autor aplicando política de reactivación si existe uno en soft delete.
/// </summary>
public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, CommandResponse<AuthorDto>>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;
    private readonly AuthorUpsertEnforcer _enforcer;

    public CreateAuthorHandler(VentusLibraryDbContext db, IMapper mapper, AuthorUpsertEnforcer enforcer)
    {
        _db = db;
        _mapper = mapper;
        _enforcer = enforcer;
    }

    /// <summary>
    /// Crea o reactiva un autor aplicando reglas de unicidad y soft delete. Retorna mensaje y datos creados.
    /// </summary>
    public async Task<CommandResponse<AuthorDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Author>(request);
        Author? existing = null;
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            existing = await _db.Set<Author>()
                .FirstOrDefaultAsync(a => a.Email == request.Email, cancellationToken);
        }

        await _enforcer.EnsureCanCreateAsync(existing, cancellationToken);

        if (existing?.IsSoftDelete == true)
        {
            entity.IsSoftDelete = false;
            entity.DeletedAt = null;
            entity.Id = existing.Id;
        }

        var entityState = existing == null
            ? EntityState.Added
            : EntityState.Modified;

        _db.Entry(entity).State = entityState;
        await _db.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<AuthorDto>(entity);
        var msg = UpsertMessage.ForCreate("Autor", existing != null);
        return new CommandResponse<AuthorDto> { Message = msg, Data = dto };
    }
}
