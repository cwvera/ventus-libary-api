using MediatR;
using VentusLibrary.Application.Authors.Dtos;

namespace VentusLibrary.Application.Authors.Queries;

/// <summary>
/// Consulta para listar autores activos (no soft-deleted).
/// </summary>
public record ListActiveAuthorsQuery : IRequest<IReadOnlyList<AuthorDto>>;
