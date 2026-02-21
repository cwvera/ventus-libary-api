using AutoMapper;
using VentusLibrary.Application.Books.Commands;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Application.Books.Mappings;

public class BookCommandProfile : Profile
{
    public BookCommandProfile()
    {
        CreateMap<CreateBookCommand, Book>()
            .ForMember(d => d.IsSoftDelete, o => o.MapFrom(_ => false))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => (DateTime?)null))
            .ForMember(d => d.DeletedAt, o => o.MapFrom(_ => (DateTime?)null));

        CreateMap<UpdateBookCommand, Book>()
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.DeletedAt, o => o.Ignore())
            .ForMember(d => d.IsSoftDelete, o => o.Ignore());
    }
}
