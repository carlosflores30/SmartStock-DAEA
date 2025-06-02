using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Smartstock.Application.Products.Dtos;
using Smartstock.Domain.Entities;
using Smartstock.Domain.Interfaces;
using Smartstock.Infrastructure.Data;

namespace Smartstock.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly SmartstockDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(SmartstockDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var entities = await _context.Products.ToListAsync();
        return _mapper.Map<IEnumerable<Product>>(entities);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Products.FindAsync(id);
        return entity == null ? null : _mapper.Map<Product>(entity);
    }

    public async Task AddAsync(Product product)
    {
        var entity = _mapper.Map<Infrastructure.PersistenceModels.Product>(product);
        await _context.Products.AddAsync(entity);
    }
    public async Task UpdateAsync(Product product)
    {
        var entity = await _context.Products.FindAsync(product.Id);
        if (entity == null) return;

        entity.Name = product.Name;
        entity.Description = product.Description;
        entity.Price = product.Price;
        entity.Stock = product.Stock;
        entity.Threshold = product.Threshold;
        entity.Categoryid = product.Categoryid;
    }
    public void Remove(Product product)
    {
        var entity = _context.Products.Find(product.Id);
        if (entity != null)
            _context.Products.Remove(entity);
    }
}

