using FluentValidation;
using VentusLibrary.Application.Authors.Commands;

namespace VentusLibrary.Application.Authors.Validators;

/// <summary>
/// Valida la solicitud de actualización de autor.
/// </summary>
public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        Include(new AuthorUpsertCommandValidator<UpdateAuthorCommand>());
    }
}
