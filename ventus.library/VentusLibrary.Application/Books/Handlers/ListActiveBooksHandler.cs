using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Application.Books.Queries;
using VentusLibrary.Infrastructure.Persistence;

namespace VentusLibrary.Application.Books.Handlers;

/// <summary>
/// Handler para listar libros activos.
/// </summary>
public class ListActiveBooksHandler : IRequestHandler<ListActiveBooksQuery, IReadOnlyList<BookDto>>
{
    private readonly VentusLibraryDbContext _db;
    private readonly IMapper _mapper;

    public ListActiveBooksHandler(VentusLibraryDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<BookDto>> Handle(ListActiveBooksQuery request, CancellationToken cancellationToken)
    {
        List<BookDto> items = await _db.Set<VentusLibrary.Domain.Entities.Book>()
            .Where(b => !b.IsSoftDelete)
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return items;
    }
}
