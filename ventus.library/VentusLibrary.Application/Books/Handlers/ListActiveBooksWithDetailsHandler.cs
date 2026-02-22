using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Application.Books.Queries;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Handlers;

/// <summary>
/// Handler que lista libros activos con detalles del autor y del género.
/// </summary>
public class ListActiveBooksWithDetailsHandler : IRequestHandler<ListActiveBooksWithDetailsQuery, IReadOnlyList<BookListItemDto>>
{
    private readonly VentusLibraryDbContext _db;

    public ListActiveBooksWithDetailsHandler(VentusLibraryDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Ejecuta la consulta realizando joins con autores y géneros activos.
    /// </summary>
    /// <param name="request">Consulta de listado con detalles.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de libros con datos de autor y género.</returns>
    public async Task<IReadOnlyList<BookListItemDto>> Handle(ListActiveBooksWithDetailsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<BookListItemDto> query =
            from b in _db.Books
            join a in _db.Authors on b.AuthorId equals a.Id
            join g in _db.Genres on b.GenreId equals g.Id
            where !b.IsSoftDelete && !a.IsSoftDelete && !g.IsSoftDelete
            select new BookListItemDto
            {
                Id = b.Id,
                Title = b.Title,
                AuthorId = a.Id,
                AuthorName = a.FullName,
                GenreId = g.Id,
                GenreName = g.Name,
                PublicationYear = b.PublicationYear,
                PageCount = b.PageCount
            };

        List<BookListItemDto> list = await query.AsNoTracking().ToListAsync(cancellationToken);
        return list;
    }
}
