using MediatR;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Categories.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
            throw new Exception("Categoría no encontrada");

        _unitOfWork.Categories.Remove(category);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}