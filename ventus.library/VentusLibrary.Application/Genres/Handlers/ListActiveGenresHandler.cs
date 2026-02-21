using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Genres.Dtos;
using VentusLibrary.Application.Genres.Queries;
using VentusLibrary.Domain.Entities;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Genres.Handlers;

/// <summary>
/// Handler para listar géneros activos.
/// </summary>
public class ListActiveGenresHandler : IRequestHandler<ListActiveGenresQuery, IReadOnlyList<GenreDto>>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;

    public ListActiveGenresHandler(VentusLibraryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    /// <summary>
    /// Lista géneros no eliminados lógicamente y proyecta a DTO.
    /// </summary>
    public async Task<IReadOnlyList<GenreDto>> Handle(ListActiveGenresQuery request, CancellationToken cancellationToken)
    {
        List<GenreDto> items = await _db.Set<Genre>()
            .Where(g => !g.IsSoftDelete)
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return items;
    }
}
