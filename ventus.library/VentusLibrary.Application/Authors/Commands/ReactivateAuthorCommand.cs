using MediatR;
using VentusLibrary.Application.Authors.Dtos;

namespace VentusLibrary.Application.Authors.Commands;

/// <summary>
/// Comando para reactivar un autor previamente eliminado lógicamente.
/// </summary>
public record ReactivateAuthorCommand(int Id) : IRequest<AuthorDto>;
