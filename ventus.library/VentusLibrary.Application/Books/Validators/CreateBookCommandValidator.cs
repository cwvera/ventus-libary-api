using FluentValidation;
using VentusLibrary.Application.Books.Commands;

namespace VentusLibrary.Application.Books.Validators;

/// <summary>
/// Valida la creación de un libro.
/// </summary>
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        Include(new BookUpsertCommandValidator<CreateBookCommand>());
    }
}
