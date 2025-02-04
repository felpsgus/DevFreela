using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project, CancellationToken cancellationToken = default);

    void Delete(Project project);

    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);

    Task<Project?> GetByIdAsync(long id, bool includeRelationships = false,
        CancellationToken cancellationToken = default);

    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default);
}