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

    public async Task<long> AddAsync(Project project)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return project.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateAsync(Project project)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task StartAsync(Project project)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            project.Start();
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task CompleteAsync(Project project)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            project.Complete();
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteAsync(Project project)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<Project?> GetByIdAsync(long id, bool includeRelationships = false)
    {
        if (includeRelationships)
        {
            return await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        return await _dbContext.Projects
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .ToListAsync();
    }
}