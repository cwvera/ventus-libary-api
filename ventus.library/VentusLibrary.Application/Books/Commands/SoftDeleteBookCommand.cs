using MediatR;

namespace VentusLibrary.Application.Books.Commands;

/// <summary>
/// Comando para realizar el borrado lógico de un libro.
/// </summary>
public record SoftDeleteBookCommand(int Id) : IRequest<bool>;
