using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly UnitOfWork _unitOfWork;

    public UserRepository(DevFreelaDbContext dbContext, UnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
            return user.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
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
}