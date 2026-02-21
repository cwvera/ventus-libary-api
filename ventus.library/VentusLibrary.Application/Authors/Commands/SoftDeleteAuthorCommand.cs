using MediatR;

namespace VentusLibrary.Application.Authors.Commands;

/// <summary>
/// Comando para realizar el borrado lógico de un autor.
/// </summary>
public record SoftDeleteAuthorCommand(int Id) : IRequest<bool>;
