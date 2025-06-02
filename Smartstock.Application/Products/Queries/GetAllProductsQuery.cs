using AutoMapper;
using MediatR;
using Smartstock.Application.Products.Dtos;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Products.Queries;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}