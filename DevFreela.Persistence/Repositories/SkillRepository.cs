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

    public async Task<long> AddAsync(Skill skill)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _dbContext.Skills.AddAsync(skill);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return skill.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateAsync(Skill skill)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Skills.Update(skill);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteAsync(Skill skill)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Skills.Remove(skill);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<Skill?> GetByIdAsync(long id)
    {
        return await _dbContext.Skills.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _dbContext.Skills.ToListAsync();
    }
}