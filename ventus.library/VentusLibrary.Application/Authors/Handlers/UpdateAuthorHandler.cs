using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Application.Authors.Services;
using VentusLibrary.Commons.Messages;
using VentusLibrary.Commons.Responses;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Authors.Handlers;

/// <summary>
/// Handler para actualizar datos de un autor activo.
/// </summary>
public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, CommandResponse<AuthorDto>>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;
    private readonly AuthorUpsertEnforcer _enforcer;

    public UpdateAuthorHandler(VentusLibraryDbContext db, IMapper mapper, AuthorUpsertEnforcer enforcer)
    {
        _db = db;
        _mapper = mapper;
        _enforcer = enforcer;
    }

    /// <summary>
    /// Actualiza datos de un autor si es válido y no eliminado; verifica unicidad de email. Retorna mensaje y datos actualizados.
    /// </summary>
    public async Task<CommandResponse<AuthorDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Set<VentusLibrary.Domain.Entities.Author>().FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (entity == null || entity.IsSoftDelete)
        {
            throw new ValidationException("El autor no existe o está eliminado.");
        }

        await _enforcer.EnsureCanUpdateAsync(request.Id, request.Email, cancellationToken);

        _mapper.Map(request, entity);

        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);
        var dto = _mapper.Map<AuthorDto>(entity);
        return new CommandResponse<AuthorDto> { Message = UpsertMessage.ForUpdate("Autor"), Data = dto };
    }
}
