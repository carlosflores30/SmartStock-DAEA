using MediatR;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<Unit>;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
        if (product == null)
            throw new Exception("Producto no encontrado");

        _unitOfWork.Products.Remove(product);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}