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

    public async Task<long> AddAsync(User user)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return user.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateAsync(User user)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteAsync(User user)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<User?> GetByIdAsync(long id, bool includeRelationships = false)
    {
        if (includeRelationships)
        {
            return await _dbContext.Users
                .Include(u => u.Skills)
                .Include(u => u.OwnedProjects).ThenInclude(p => p.Client)
                .Include(u => u.OwnedProjects).ThenInclude(p => p.Freelancer)
                .Include(u => u.FreelanceProjects).ThenInclude(p => p.Client)
                .Include(u => u.FreelanceProjects).ThenInclude(p => p.Freelancer)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        return await _dbContext.Users
            .IgnoreAutoIncludes()
            .SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
}