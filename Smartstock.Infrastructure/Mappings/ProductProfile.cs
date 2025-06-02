using AutoMapper;
using Smartstock.Application.Products.Dtos;
using Smartstock.Domain.Entities;

namespace Smartstock.Infrastructure.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}