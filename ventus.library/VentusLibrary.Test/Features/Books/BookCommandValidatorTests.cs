using FluentValidation.Results;
using VentusLibrary.Application.Books.Commands;
using VentusLibrary.Application.Books.Validators;

namespace VentusLibrary.Test.Features.Books;

/// <summary>
/// Pruebas unitarias de validadores para comandos de libros.
/// </summary>
public class BookCommandValidatorTests
{
    /// <summary>
    /// Verifica que un CreateBookCommand válido pasa la validación.
    /// </summary>
    [Fact]
    public async Task Create_Valid_Command_Passes_Validation()
    {
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        CreateBookCommand cmd = new CreateBookCommand(
            "Título válido",
            "Descripción",
            -300,
            5,
            12,
            100);
        ValidationResult result = await validator.ValidateAsync(cmd, CancellationToken.None);
        Assert.True(result.IsValid);
    }

    /// <summary>
    /// Verifica que Title vacío en CreateBookCommand produce error.
    /// </summary>
    [Fact]
    public async Task Create_Invalid_Title_Empty_Fails()
    {
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        CreateBookCommand cmd = new CreateBookCommand(
            "",
            null,
            null,
            5,
            12,
            1);
        ValidationResult result = await validator.ValidateAsync(cmd, CancellationToken.None);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    /// <summary>
    /// Verifica que UpdateBookCommand con Id inválido falla.
    /// </summary>
    [Fact]
    public async Task Update_Invalid_Id_Fails()
    {
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        UpdateBookCommand cmd = new UpdateBookCommand(
            0,
            "Título",
            null,
            null,
            5,
            12,
            10);
        ValidationResult result = await validator.ValidateAsync(cmd, CancellationToken.None);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }
}
