using AutoMapper;
using Smartstock.Domain.Entities;

namespace Smartstock.Infrastructure.Mappings;

public class ProductInfraToDomainProfile : Profile
{
    public ProductInfraToDomainProfile()
    {
        CreateMap<Smartstock.Infrastructure.PersistenceModels.Product, Product>().ReverseMap();
    }
}