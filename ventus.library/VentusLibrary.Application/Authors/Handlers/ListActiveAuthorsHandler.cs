using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Application.Authors.Queries;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Authors.Handlers;

/// <summary>
/// Handler para listar autores activos.
/// </summary>
public class ListActiveAuthorsHandler : IRequestHandler<ListActiveAuthorsQuery, IReadOnlyList<AuthorDto>>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;

    public ListActiveAuthorsHandler(VentusLibraryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    /// <summary>
    /// Lista autores no eliminados lógicamente y proyecta a DTO.
    /// </summary>
    public async Task<IReadOnlyList<AuthorDto>> Handle(ListActiveAuthorsQuery request, CancellationToken cancellationToken)
    {
        var items = await _db.Set<VentusLibrary.Domain.Entities.Author>()
            .Where(a => !a.IsSoftDelete)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return items;
    }
}
