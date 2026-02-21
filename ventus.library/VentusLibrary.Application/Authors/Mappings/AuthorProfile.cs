using AutoMapper;
using VentusLibrary.Application.Authors.Dtos;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Application.Authors.Mappings;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
    }
}
