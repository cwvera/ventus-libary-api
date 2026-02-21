using MediatR;
using VentusLibrary.Application.Genres.Dtos;

namespace VentusLibrary.Application.Genres.Queries;

/// <summary>
/// Consulta para listar géneros activos (no soft-deleted).
/// </summary>
public record ListActiveGenresQuery : IRequest<IReadOnlyList<GenreDto>>;
