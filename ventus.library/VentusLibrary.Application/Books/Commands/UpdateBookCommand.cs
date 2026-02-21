using MediatR;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Commons.Responses;

namespace VentusLibrary.Application.Books.Commands;

/// <summary>
/// Comando para actualizar un libro existente.
/// </summary>
public record UpdateBookCommand(
    int Id,
    string Title,
    string? Description,
    int? PublicationYear,
    int GenreId,
    int AuthorId,
    int PageCount
) : IRequest<CommandResponse<BookDto>>, IBookUpsertCommand;
