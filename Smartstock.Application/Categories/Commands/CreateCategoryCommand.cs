using MediatR;
using Smartstock.Domain.Entities;
using Smartstock.Domain.Interfaces;

namespace Smartstock.Application.Categories.Commands;

public record CreateCategoryCommand(string Name, string? Description) :  IRequest<Guid>;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Createdat = DateTime.Now
        };

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.CompleteAsync();

        return category.Id;
    }
}