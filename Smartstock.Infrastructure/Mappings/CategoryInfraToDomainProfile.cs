using AutoMapper;
using Smartstock.Domain.Entities;

namespace Smartstock.Infrastructure.Mappings;

public class CategoryInfraToDomainProfile : Profile
{
    public CategoryInfraToDomainProfile()
    {
        CreateMap<Smartstock.Infrastructure.PersistenceModels.Category, Category>().ReverseMap();
    }
}