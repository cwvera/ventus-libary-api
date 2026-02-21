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
/// Handler para obtener el detalle de un género por Id (solo activos).
/// </summary>
public class GetGenreByIdHandler : IRequestHandler<GetGenreByIdQuery, GenreDto?>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;

    public GetGenreByIdHandler(VentusLibraryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene un género activo por Id y lo proyecta a DTO; retorna null si no existe.
    /// </summary>
    public async Task<GenreDto?> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        GenreDto? dto = await _db.Set<Genre>()
            .Where(g => g.Id == request.Id && !g.IsSoftDelete)
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        return dto;
    }
}
