using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Persistence.Context;

namespace DevFreela.Persistence.Repositories;

public class ProjectCommentRepository : IProjectCommentRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectCommentRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> AddAsync(ProjectComment projectComment, CancellationToken cancellationToken = default)
    {
        await _dbContext.ProjectComments.AddAsync(projectComment, cancellationToken);
        return projectComment.Id;
    }
}