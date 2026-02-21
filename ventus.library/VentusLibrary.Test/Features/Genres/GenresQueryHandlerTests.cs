using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Genres.Dtos;
using VentusLibrary.Application.Genres.Handlers;
using VentusLibrary.Application.Genres.Queries;
using VentusLibrary.Application.Genres.Mappings;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;
using Xunit;

namespace VentusLibrary.Test.Features.Genres;

/// <summary>
/// Pruebas unitarias de handlers de queries para géneros.
/// </summary>
public class GenresQueryHandlerTests
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
    /// Crea configuración de AutoMapper para proyecciones a GenreDto.
    /// </summary>
    private static IMapper CreateMapper()
    {
        MapperConfiguration cfg = new MapperConfiguration(c =>
        {
            c.AddProfile(new GenreProfile());
        });
        return cfg.CreateMapper();
    }

    /// <summary>
    /// Retorna DTO cuando el género está activo.
    /// </summary>
    [Fact]
    public async Task GetById_Returns_Dto_When_Active()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Genre>().Add(new Genre { Id = 20, Name = "Historia", IsSoftDelete = false });
        await db.SaveChangesAsync();

        GetGenreByIdHandler handler = new GetGenreByIdHandler(db, mapper);
        GenreDto? dto = await handler.Handle(new GetGenreByIdQuery(20), CancellationToken.None);
        Assert.NotNull(dto);
        Assert.Equal(20, dto!.Id);
    }

    /// <summary>
    /// Retorna null cuando el género está eliminado lógicamente.
    /// </summary>
    [Fact]
    public async Task GetById_Returns_Null_When_SoftDeleted()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Genre>().Add(new Genre { Id = 21, Name = "Novela", IsSoftDelete = true });
        await db.SaveChangesAsync();

        GetGenreByIdHandler handler = new GetGenreByIdHandler(db, mapper);
        GenreDto? dto = await handler.Handle(new GetGenreByIdQuery(21), CancellationToken.None);
        Assert.Null(dto);
    }

    /// <summary>
    /// Lista solo géneros activos.
    /// </summary>
    [Fact]
    public async Task ListActive_Returns_Only_Active()
    {
        VentusLibraryDbContext db = CreateDb();
        IMapper mapper = CreateMapper();
        db.Set<Genre>().AddRange(
        [
            new Genre { Id = 1, Name = "A", IsSoftDelete = false },
            new Genre { Id = 2, Name = "B", IsSoftDelete = true },
            new Genre { Id = 3, Name = "C", IsSoftDelete = false },
        ]);
        await db.SaveChangesAsync();

        ListActiveGenresHandler handler = new ListActiveGenresHandler(db, mapper);
        IReadOnlyList<GenreDto> items = await handler.Handle(new ListActiveGenresQuery(), CancellationToken.None);
        Assert.Equal(2, items.Count);
        Assert.DoesNotContain(items, g => g.Id == 2);
    }
}
