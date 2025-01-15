using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly UnitOfWork _unitOfWork;

    public SkillRepository(DevFreelaDbContext dbContext, UnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> AddAsync(Skill skill, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _dbContext.Skills.AddAsync(skill, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
            return skill.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateAsync(Skill skill, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Skills.Update(skill);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteAsync(Skill skill, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Skills.Remove(skill);
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
        return await _dbContext.Skills.AnyAsync(s => s.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Skill?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Skills.SingleOrDefaultAsync(s => s.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<List<Skill>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Skills.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> CheckDescriptionAsync(string description, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Skills.AnyAsync(s => s.Description == description,
            cancellationToken: cancellationToken);
    }
}