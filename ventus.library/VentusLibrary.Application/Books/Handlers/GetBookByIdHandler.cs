using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Application.Books.Queries;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Handlers;

/// <summary>
/// Handler para obtener el detalle de un libro por Id (solo activos).
/// </summary>
public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;

    public GetBookByIdHandler(VentusLibraryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        BookDto? dto = await _db.Set<VentusLibrary.Domain.Entities.Book>()
            .Where(b => b.Id == request.Id && !b.IsSoftDelete)
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        return dto;
    }
}
