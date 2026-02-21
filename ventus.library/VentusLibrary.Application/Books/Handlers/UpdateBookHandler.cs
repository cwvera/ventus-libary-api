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
/// Handler para actualizar datos de un libro activo.
/// </summary>
public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, CommandResponse<BookDto>>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;
    private readonly BookLimitEnforcer _enforcer;

    public UpdateBookHandler(VentusLibraryDbContext db, BookLimitEnforcer enforcer, IMapper mapper)
    {
        _db = db;
        _enforcer = enforcer;
        _mapper = mapper;
    }

    /// <summary>
    /// Actualiza un libro existente tras validar límites y referencias. Retorna mensaje y datos actualizados.
    /// </summary>
    public async Task<CommandResponse<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await _enforcer.EnsureCanUpsertAsync(request.Id, request.AuthorId, request.GenreId, cancellationToken);

        Book entity = _mapper.Map<Book>(request);
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);
        BookDto dto = _mapper.Map<BookDto>(entity);
        return new CommandResponse<BookDto> { Message = UpsertMessage.ForUpdate("Libro"), Data = dto };
    }
}
