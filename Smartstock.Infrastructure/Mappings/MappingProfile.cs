using AutoMapper;
using Smartstock.Application.Products.Dtos;
using EfProduct = Smartstock.Infrastructure.PersistenceModels.Product;
using DomainUser = Smartstock.Domain.Entities.User;
using EfUser = Smartstock.Infrastructure.PersistenceModels.User;

namespace Smartstock.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<EfUser, DomainUser>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Createdat))
            .ReverseMap()
            .ForMember(dest => dest.Createdat, opt => opt.MapFrom(src => src.CreatedAt));
    }
}