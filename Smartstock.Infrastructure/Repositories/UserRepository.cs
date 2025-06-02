using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Smartstock.Domain.Entities;
using Smartstock.Domain.Interfaces;
using Smartstock.Infrastructure.Data;
using EfUser = Smartstock.Infrastructure.PersistenceModels.User;

namespace Smartstock.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SmartstockDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(SmartstockDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var efUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return efUser is null ? null : _mapper.Map<User>(efUser);
    }

    public async Task AddAsync(User user)
    {
        var efUser = _mapper.Map<EfUser>(user);
        await _context.Users.AddAsync(efUser);
    }
}