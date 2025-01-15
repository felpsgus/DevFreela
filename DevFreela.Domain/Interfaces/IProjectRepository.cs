using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface IProjectRepository
{
    Task<long> AddAsync(Project project, CancellationToken cancellationToken = default);

    Task UpdateAsync(Project project, CancellationToken cancellationToken = default);

    Task StartAsync(Project project, CancellationToken cancellationToken = default);

    Task CompleteAsync(Project project, CancellationToken cancellationToken = default);

    Task DeleteAsync(Project project, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);

    Task<Project?> GetByIdAsync(long id, bool includeRelationships = false,
        CancellationToken cancellationToken = default);

    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default);
}