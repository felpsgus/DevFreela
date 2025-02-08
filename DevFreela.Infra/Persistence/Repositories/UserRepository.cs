using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public UserRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(long id, bool includeRelationships = false,
        CancellationToken cancellationToken = default)
    {
        if (includeRelationships)
        {
            return await _dbContext.Users
                .IgnoreAutoIncludes()
                .Include(u => u.Skills)
                .Include(u => u.OwnedProjects).ThenInclude(p => p.Client)
                .Include(u => u.OwnedProjects).ThenInclude(p => p.Freelancer)
                .Include(u => u.FreelanceProjects).ThenInclude(p => p.Client)
                .Include(u => u.FreelanceProjects).ThenInclude(p => p.Freelancer)
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
        }

        return await _dbContext.Users
            .IgnoreAutoIncludes()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByCredentials(string email, string password, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password, cancellationToken);
    }
}