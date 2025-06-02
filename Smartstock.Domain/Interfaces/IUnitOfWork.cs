namespace Smartstock.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    Task<int> SaveAsync();
    
    IProductRepository Products { get; }
    Task<int> CompleteAsync();
    ICategoryRepository Categories { get; }
}