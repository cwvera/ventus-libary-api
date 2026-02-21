using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Application.Authors.Queries;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Authors.Handlers;

/// <summary>
/// Handler para obtener el detalle de un autor por Id (solo activos).
/// </summary>
public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;

    public GetAuthorByIdHandler(VentusLibraryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene un autor activo por Id y lo proyecta a DTO; retorna null si no existe.
    /// </summary>
    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _db.Set<VentusLibrary.Domain.Entities.Author>()
            .Where(a => a.Id == request.Id && !a.IsSoftDelete)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
}
