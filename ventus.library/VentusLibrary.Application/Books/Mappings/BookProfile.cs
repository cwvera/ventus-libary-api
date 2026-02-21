using AutoMapper;
using VentusLibrary.Application.Books.Dtos;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Application.Books.Mappings;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
    }
}
