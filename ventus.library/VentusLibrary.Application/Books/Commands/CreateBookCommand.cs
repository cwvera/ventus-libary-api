using MediatR;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Commons.Responses;

namespace VentusLibrary.Application.Books.Commands;

/// <summary>
/// Comando para crear un libro aplicando las políticas de BookLimit activas.
/// </summary>
public record CreateBookCommand(
    string Title,
    string? Description,
    int? PublicationYear,
    int GenreId,
    int AuthorId,
    int PageCount
) : IRequest<CommandResponse<BookDto>>, IBookUpsertCommand;
