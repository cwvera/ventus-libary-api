using MediatR;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Commons.Responses;

namespace VentusLibrary.Application.Authors.Commands;

/// <summary>
/// Comando para crear un autor. Si existe uno con Email/FullName en soft delete, debe reactivarse.
/// </summary>
public record CreateAuthorCommand(
    string FullName,
    string? Description,
    DateTime? BirthDate,
    string? City,
    string? Email
) : IRequest<CommandResponse<AuthorDto>>, IAuthorUpsertCommand;
