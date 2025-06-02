using AutoMapper;
using Smartstock.Domain.Entities;


namespace Smartstock.Infrastructure.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, Smartstock.Application.Categories.Dtos.CategoryDto>().ReverseMap();
    }
}