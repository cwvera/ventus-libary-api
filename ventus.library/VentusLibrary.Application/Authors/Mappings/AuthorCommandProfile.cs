using AutoMapper;
using VentusLibrary.Application.Authors.Commands;
using VentusLibrary.Domain.Entities;

namespace VentusLibrary.Application.Authors.Mappings;

public class AuthorCommandProfile : Profile
{
    public AuthorCommandProfile()
    {
        CreateMap<CreateAuthorCommand, Author>()
            .ForMember(d => d.IsSoftDelete, o => o.MapFrom(_ => false))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => (DateTime?)null))
            .ForMember(d => d.DeletedAt, o => o.MapFrom(_ => (DateTime?)null));

        CreateMap<UpdateAuthorCommand, Author>()
            .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.DeletedAt, o => o.Ignore())
            .ForMember(d => d.IsSoftDelete, o => o.Ignore());
    }
}
