using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Smartstock.Domain.Entities;
using Smartstock.Domain.Interfaces;
using Smartstock.Infrastructure.Data;

namespace Smartstock.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly SmartstockDbContext _context;
    private readonly IMapper _mapper;
    
    public CategoryRepository(SmartstockDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return _mapper.Map<IEnumerable<Category>>(categories);
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Categories.FindAsync(id);
        return entity == null ? null : _mapper.Map<Category>(entity);
    }

    public async Task AddAsync(Category category)
    {
        var entity = _mapper.Map<Smartstock.Infrastructure.PersistenceModels.Category>(category);
        await _context.Categories.AddAsync(entity);
    }

    public void Remove(Category category)
    {
        var entity = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
        if (entity != null)
        {
            _context.Categories.Remove(entity);
        }
    }
}