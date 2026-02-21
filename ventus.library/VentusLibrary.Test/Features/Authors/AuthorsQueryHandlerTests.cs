using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Application.Authors.Handlers;
using VentusLibrary.Application.Authors.Queries;
using VentusLibrary.Application.Authors.Mappings;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Test.Features.Authors;

/// <summary>
/// Pruebas unitarias de handlers de queries para autores.
/// </summary>
public class AuthorsQueryHandlerTests
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
    /// Crea configuración de AutoMapper para proyecciones a AuthorDto.
    /// </summary>
    private static IMapper CreateMapper()
    {
        MapperConfiguration cfg = new MapperConfiguration(c =>
        {
            c.AddProfile(new AuthorProfile());
        });
        return cfg.CreateMapper();
    }

    /// <summary>
    /// Retorna DTO cuando el autor está activo.
    /// </summary>
    [Fact]
    public async Task GetById_Returns_Dto_When_Active()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Author>().Add(new Author { Id = 10, FullName = "Jane Doe", IsSoftDelete = false });
        await db.SaveChangesAsync();

        GetAuthorByIdHandler handler = new GetAuthorByIdHandler(db, mapper);
        AuthorDto? dto = await handler.Handle(new GetAuthorByIdQuery(10), CancellationToken.None);
        Assert.NotNull(dto);
        Assert.Equal(10, dto!.Id);
    }

    /// <summary>
    /// Retorna null cuando el autor está eliminado lógicamente.
    /// </summary>
    [Fact]
    public async Task GetById_Returns_Null_When_SoftDeleted()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Author>().Add(new Author { Id = 11, FullName = "John Doe", IsSoftDelete = true });
        await db.SaveChangesAsync();

        GetAuthorByIdHandler handler = new GetAuthorByIdHandler(db, mapper);
        AuthorDto? dto = await handler.Handle(new GetAuthorByIdQuery(11), CancellationToken.None);
        Assert.Null(dto);
    }

    /// <summary>
    /// Lista solo autores activos.
    /// </summary>
    [Fact]
    public async Task ListActive_Returns_Only_Active()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Author>().AddRange(
        [
            new Author { Id = 1, FullName = "A", IsSoftDelete = false },
            new Author { Id = 2, FullName = "B", IsSoftDelete = true },
            new Author { Id = 3, FullName = "C", IsSoftDelete = false },
        ]);
        await db.SaveChangesAsync();

        ListActiveAuthorsHandler handler = new ListActiveAuthorsHandler(db, mapper);
        IReadOnlyList<AuthorDto> items = await handler.Handle(new ListActiveAuthorsQuery(), CancellationToken.None);
        Assert.Equal(2, items.Count);
        Assert.DoesNotContain(items, a => a.Id == 2);
    }
}
