using MediatR;
using VentusLibrary.Application.Books.Dtos;

namespace VentusLibrary.Application.Books.Queries;

/// <summary>
/// Consulta para obtener un libro por identificador.
/// </summary>
public record GetBookByIdQuery(int Id) : IRequest<BookDto?>;
