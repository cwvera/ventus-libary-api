using MediatR;
using VentusLibrary.Application.Genres.Dtos;

namespace VentusLibrary.Application.Genres.Queries;

/// <summary>
/// Consulta para obtener un género por identificador.
/// </summary>
public record GetGenreByIdQuery(int Id) : IRequest<GenreDto?>;
