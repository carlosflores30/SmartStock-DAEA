using AutoMapper;
using MediatR;
using Smartstock.Application.Products.Dtos;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
        if (product == null)
            throw new Exception("Producto no encontrado");

        return _mapper.Map<ProductDto>(product);
    }
}