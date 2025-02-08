using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await _dbContext.Projects.AddAsync(project, cancellationToken);
    }

    public void Delete(Project project)
    {
        _dbContext.Projects.Remove(project);
    }

    public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Projects.AnyAsync(p => p.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(long id, bool includeRelationships = false,
        CancellationToken cancellationToken = default)
    {
        if (includeRelationships)
        {
            return await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        }

        return await _dbContext.Projects
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}