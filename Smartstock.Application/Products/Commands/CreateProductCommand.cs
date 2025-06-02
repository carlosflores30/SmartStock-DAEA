using MediatR;
using Smartstock.Domain.Entities;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Products.Commands;

public record CreateProductCommand(string Name, string? Description, decimal Price, int Stock, int Threshold, Guid? Categoryid) : IRequest<Guid>;

public class CreateProductHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.Categoryid != null)
        {
            var categoryExists = await _unitOfWork.Categories.GetByIdAsync(request.Categoryid.Value);
            if (categoryExists == null)
                throw new Exception("La categoría especificada no existe");
        }
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            Threshold = request.Threshold,
            Categoryid = request.Categoryid,
            Createdat = DateTime.Now
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.CompleteAsync();

        return product.Id;
    }
}