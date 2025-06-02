using Smartstock.Domain.Entities;

namespace Smartstock.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task AddAsync(Category category);
    void Remove(Category category);
}