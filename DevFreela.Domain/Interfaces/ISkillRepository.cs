using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface ISkillRepository
{
    Task<long> AddAsync(Skill user, CancellationToken cancellationToken = default);

    Task UpdateAsync(Skill user, CancellationToken cancellationToken = default);

    Task DeleteAsync(Skill user, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);

    Task<Skill?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<List<Skill>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> CheckDescriptionAsync(string description, CancellationToken cancellationToken = default);
}