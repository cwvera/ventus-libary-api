namespace VentusLibrary.Application.Books.Commands;

public interface IBookUpsertCommand
{
    string Title { get; }
    string? Description { get; }
    int? PublicationYear { get; }
    int GenreId { get; }
    int AuthorId { get; }
    int PageCount { get; }
}
