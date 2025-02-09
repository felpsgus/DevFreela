using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);

    void Delete(User user);

    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(long id, bool includeRelationships = false, CancellationToken cancellationToken = default);

    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<User?> CheckEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> GetUserByCredentials(string email, string password, CancellationToken cancellationToken = default);
}