using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Application.Books.Handlers;
using VentusLibrary.Application.Books.Queries;
using VentusLibrary.Application.Books.Mappings;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Test.Features.Books;

/// <summary>
/// Pruebas unitarias de handlers de queries para libros.
/// </summary>
public class BooksQueryHandlerTests
{
    /// <summary>
    /// Crea un DbContext en memoria para aislar los tests.
    /// </summary>
    private static VentusLibraryDbContext CreateDb()
    {
        DbContextOptions<VentusLibraryDbContext> options = new DbContextOptionsBuilder<VentusLibraryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new VentusLibraryDbContext(options);
    }

    /// <summary>
    /// Crea configuración de AutoMapper para proyecciones a BookDto.
    /// </summary>
    private static IMapper CreateMapper()
    {
        MapperConfiguration cfg = new MapperConfiguration(c =>
        {
            c.AddProfile(new BookProfile());
        });
        return cfg.CreateMapper();
    }

    /// <summary>
    /// Retorna DTO cuando el libro está activo.
    /// </summary>
    [Fact]
    public async Task GetById_Returns_Dto_When_Active()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Book>().Add(new Book { Id = 1, Title = "T1", PageCount = 100, GenreId = 2, AuthorId = 3, IsSoftDelete = false });
        await db.SaveChangesAsync();

        GetBookByIdHandler handler = new GetBookByIdHandler(db, mapper);
        BookDto? dto = await handler.Handle(new GetBookByIdQuery(1), CancellationToken.None);
        Assert.NotNull(dto);
        Assert.Equal(1, dto!.Id);
    }

    /// <summary>
    /// Retorna null cuando el libro está eliminado lógicamente.
    /// </summary>
    [Fact]
    public async Task GetById_Returns_Null_When_SoftDeleted()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Book>().Add(new Book { Id = 2, Title = "T2", PageCount = 120, GenreId = 2, AuthorId = 3, IsSoftDelete = true });
        await db.SaveChangesAsync();

        GetBookByIdHandler handler = new GetBookByIdHandler(db, mapper);
        BookDto? dto = await handler.Handle(new GetBookByIdQuery(2), CancellationToken.None);
        Assert.Null(dto);
    }

    /// <summary>
    /// Lista solo libros activos.
    /// </summary>
    [Fact]
    public async Task ListActive_Returns_Only_Active()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Book>().AddRange(
        [
            new Book { Id = 1, Title = "A", PageCount = 50, GenreId = 1, AuthorId = 1, IsSoftDelete = false },
            new Book { Id = 2, Title = "B", PageCount = 60, GenreId = 1, AuthorId = 1, IsSoftDelete = true },
            new Book { Id = 3, Title = "C", PageCount = 70, GenreId = 1, AuthorId = 1, IsSoftDelete = false },
        ]);
        await db.SaveChangesAsync();

        ListActiveBooksHandler handler = new ListActiveBooksHandler(db, mapper);
        IReadOnlyList<BookDto> items = await handler.Handle(new ListActiveBooksQuery(), CancellationToken.None);
        Assert.Equal(2, items.Count);
        Assert.DoesNotContain(items, b => b.Id == 2);
    }
}
