using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Persistence.Context;

namespace DevFreela.Persistence.Repositories;

public class ProjectCommentRepository : IProjectCommentRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly UnitOfWork _unitOfWork;

    public ProjectCommentRepository(DevFreelaDbContext dbContext, UnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> AddAsync(ProjectComment projectComment, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _dbContext.ProjectComments.AddAsync(projectComment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
            return projectComment.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}