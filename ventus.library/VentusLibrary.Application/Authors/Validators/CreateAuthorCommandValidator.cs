using FluentValidation;
using VentusLibrary.Application.Authors.Commands;

namespace VentusLibrary.Application.Authors.Validators;

/// <summary>
/// Valida la solicitud de creación de autor según reglas de negocio y límites de esquema.
/// </summary>
public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        Include(new AuthorUpsertCommandValidator<CreateAuthorCommand>());
    }
}
