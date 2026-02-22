using MediatR;
using VentusLibrary.Application.Books.Dtos;

namespace VentusLibrary.Application.Books.Queries;

/// <summary>
/// Consulta para listar libros activos incluyendo datos del autor y del género.
/// </summary>
public record ListActiveBooksWithDetailsQuery() : IRequest<IReadOnlyList<BookListItemDto>>;
