using MediatR;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Products.Commands;

public record UpdateProductCommand(Guid Id, string Name, string? Description, decimal Price, int Stock, int Threshold, Guid? Categoryid) : IRequest<Unit>;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
        if (product == null)
            throw new Exception("Producto no encontrado");
        
        if (request.Categoryid != null)
        {
            var categoryExists = await _unitOfWork.Categories.GetByIdAsync(request.Categoryid.Value);
            if (categoryExists == null)
                throw new Exception("La categoría especificada no existe");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.Threshold = request.Threshold;
        product.Categoryid = request.Categoryid;

        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.CompleteAsync();
        return Unit.Value;
    }
}