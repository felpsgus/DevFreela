using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface IUserRepository
{
    Task<long> AddAsync(User user, CancellationToken cancellationToken = default);

    void Delete(User user);

    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(long id, bool includeRelationships = false, CancellationToken cancellationToken = default);

    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default);
}