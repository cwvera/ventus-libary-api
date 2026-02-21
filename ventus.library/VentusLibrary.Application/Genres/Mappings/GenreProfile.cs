using AutoMapper;
using VentusLibrary.Application.Genres.Dtos;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Application.Genres.Mappings;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
    }
}
