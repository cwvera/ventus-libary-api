using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Application.Authors.Validators;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace VentusLibrary.Test.Features.Authors;

/// <summary>
/// Pruebas unitarias de validadores para comandos de autores.
/// </summary>
public class AuthorCommandValidatorTests
{
    /// <summary>
    /// Verifica que un CreateAuthorCommand válido pasa la validación.
    /// </summary>
    [Fact]
    public async Task Create_Valid_Command_Passes_Validation()
    {
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        CreateAuthorCommand cmd = new CreateAuthorCommand(
            "Jane Doe",
            "Autora",
            new DateTime(1990, 1, 1),
            "Madrid",
            "jane@example.com");
        ValidationResult result = await validator.ValidateAsync(cmd, CancellationToken.None);
        Assert.True(result.IsValid);
    }

    /// <summary>
    /// Verifica que FullName vacío en CreateAuthorCommand produce error.
    /// </summary>
    [Fact]
    public async Task Create_Invalid_FullName_Empty_Fails()
    {
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        CreateAuthorCommand cmd = new CreateAuthorCommand(
            "",
            null,
            null,
            null,
            null);
        ValidationResult result = await validator.ValidateAsync(cmd, CancellationToken.None);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "FullName");
    }

    /// <summary>
    /// Verifica que UpdateAuthorCommand con Id inválido falla.
    /// </summary>
    [Fact]
    public async Task Update_Invalid_Id_Fails()
    {
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        UpdateAuthorCommand cmd = new UpdateAuthorCommand(
            0,
            "Jane Doe",
            null,
            null,
            null,
            null);
        ValidationResult result = await validator.ValidateAsync(cmd, CancellationToken.None);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }
}
