using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly UnitOfWork _unitOfWork;

    public ProjectRepository(DevFreelaDbContext dbContext, UnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _dbContext.Projects.AddAsync(project, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
            return project.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task StartAsync(Project project, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            project.Start();
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task CompleteAsync(Project project, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            project.Complete();
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteAsync(Project project, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Projects.Remove(project);
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