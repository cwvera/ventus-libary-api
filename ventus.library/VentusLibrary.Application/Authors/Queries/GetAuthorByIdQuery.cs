using MediatR;
using VentusLibrary.Application.Authors.Dtos;

namespace VentusLibrary.Application.Authors.Queries;

/// <summary>
/// Consulta para obtener el detalle de un autor por su identificador.
/// </summary>
public record GetAuthorByIdQuery(int Id) : IRequest<AuthorDto?>;
