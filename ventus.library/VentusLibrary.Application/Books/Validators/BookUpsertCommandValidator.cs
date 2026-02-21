using FluentValidation;
using VentusLibrary.Application.Books.Commands;

namespace VentusLibrary.Application.Books.Validators;

public class BookUpsertCommandValidator<T> : AbstractValidator<T> where T : IBookUpsertCommand
{

    public BookUpsertCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .When(x => x.Description != null);

        RuleFor(x => x.PageCount)
            .GreaterThan(0);

        RuleFor(x => x.PublicationYear)
            .Must(y => y == null || (y >= -3000 && y <= DateTime.UtcNow.Year))
            .WithMessage($"El año de publicación debe estar entre 3000 A.C. y {DateTime.Today.Year:yyyy}");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0);

        RuleFor(x => x.GenreId)
            .GreaterThan(0);
    }
}
