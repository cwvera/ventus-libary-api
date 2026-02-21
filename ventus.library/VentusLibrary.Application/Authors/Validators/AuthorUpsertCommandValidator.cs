using FluentValidation;
using VentusLibrary.Application.Authors.Commands;

namespace VentusLibrary.Application.Authors.Validators;

public class AuthorUpsertCommandValidator<T> : AbstractValidator<T> where T : IAuthorUpsertCommand
{
    public AuthorUpsertCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .When(x => x.Description != null);

        RuleFor(x => x.City)
            .MaximumLength(100)
            .When(x => x.City != null);

        RuleFor(x => x.Email)
            .MaximumLength(100)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.BirthDate)
            .Must(date => date == null || date <= DateTime.UtcNow.Date)
            .WithMessage("La fecha de nacimiento no puede ser futura.");
    }
}
