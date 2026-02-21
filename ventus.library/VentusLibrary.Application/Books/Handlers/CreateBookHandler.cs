using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Commands;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Application.Books.Services.BookLimits.Builders;
using VentusLibrary.Commons.Messages;
using VentusLibrary.Commons.Responses;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Handlers;

/// <summary>
/// Handler para crear un libro validando referencias y límites.
/// </summary>
public class CreateBookHandler : IRequestHandler<CreateBookCommand, CommandResponse<BookDto>>
{
    private readonly VentusLibraryDbContext _db;
    private readonly BookLimitEnforcer _enforcer;
    private readonly IMapper _mapper;

    public CreateBookHandler(VentusLibraryDbContext db, BookLimitEnforcer enforcer, IMapper mapper)
    {
        _db = db;
        _enforcer = enforcer;
        _mapper = mapper;
    }

    /// <summary>
    /// Crea o reactiva un libro según reglas de negocio. Retorna mensaje y datos creados.
    /// </summary>
    public async Task<CommandResponse<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _enforcer.EnsureCanUpsertAsync(null, request.AuthorId, request.GenreId, cancellationToken);

        Book entity = _mapper.Map<Book>(request);
        Book? existing = await _db.Set<Book>()
            .FirstOrDefaultAsync(b => b.Title == request.Title && b.AuthorId == request.AuthorId, cancellationToken);

        if (existing?.IsSoftDelete == true)
        {
            entity.IsSoftDelete = false;
            entity.DeletedAt = null;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Id = existing.Id;
        }

        EntityState entityState = existing == null ? EntityState.Added : EntityState.Modified;
        _db.Entry(entity).State = entityState;
        await _db.SaveChangesAsync(cancellationToken);
        BookDto dto = _mapper.Map<BookDto>(entity);
        string msg = UpsertMessage.ForCreate("Libro", existing != null);
        return new CommandResponse<BookDto> { Message = msg, Data = dto };
    }
}
