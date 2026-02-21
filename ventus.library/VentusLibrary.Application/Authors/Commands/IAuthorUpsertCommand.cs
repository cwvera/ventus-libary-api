namespace VentusLibrary.Application.Authors.Commands;

public interface IAuthorUpsertCommand
{
    string FullName { get; }
    string? Description { get; }
    DateTime? BirthDate { get; }
    string? City { get; }
    string? Email { get; }
}
