using AutoMapper;
using Smartstock.Domain.Interfaces;
using Smartstock.Infrastructure.Repositories;

namespace Smartstock.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SmartstockDbContext _context;

    public UnitOfWork(SmartstockDbContext context, IMapper mapper)
    {
        _context = context;
        UserRepository = new UserRepository(_context, mapper);
        Products = new ProductRepository(context, mapper);
        Categories = new CategoryRepository(context, mapper);
    }

    public IUserRepository UserRepository { get; }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
}