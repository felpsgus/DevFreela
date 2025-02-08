using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public SkillRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Skill skill, CancellationToken cancellationToken = default)
    {
        await _dbContext.Skills.AddAsync(skill, cancellationToken);
    }

    public void Delete(Skill skill)
    {
        _dbContext.Skills.Remove(skill);
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