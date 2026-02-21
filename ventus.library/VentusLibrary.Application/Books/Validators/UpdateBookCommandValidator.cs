using FluentValidation;
using VentusLibrary.Application.Books.Commands;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Validators;

/// <summary>
/// Valida la actualización de un libro.
/// </summary>
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{

    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        Include(new BookUpsertCommandValidator<UpdateBookCommand>());
    }
}
