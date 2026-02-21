using MediatR;
using VentusLibrary.Application.Books.Dtos;

namespace VentusLibrary.Application.Books.Queries;

/// <summary>
/// Consulta para listar libros activos (no soft-deleted).
/// </summary>
public record ListActiveBooksQuery : IRequest<IReadOnlyList<BookDto>>;
