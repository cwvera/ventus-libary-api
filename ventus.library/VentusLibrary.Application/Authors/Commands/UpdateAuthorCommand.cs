using MediatR;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Commons.Responses;

namespace VentusLibrary.Application.Authors.Commands;

/// <summary>
/// Comando para actualizar los datos de un autor existente.
/// </summary>
public record UpdateAuthorCommand(
    int Id,
    string FullName,
    string? Description,
    DateTime? BirthDate,
    string? City,
    string? Email
) : IRequest<CommandResponse<AuthorDto>>, IAuthorUpsertCommand;
