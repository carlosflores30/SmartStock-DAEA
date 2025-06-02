using AutoMapper;
using MediatR;
using Smartstock.Application.Categories.Dtos;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Categories.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
            throw new Exception("Categoría no encontrada");

        return _mapper.Map<CategoryDto>(category);
    }
}