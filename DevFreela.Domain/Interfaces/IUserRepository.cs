using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface IUserRepository
{
    Task<long> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User?> GetByIdAsync(long id, bool includeRelationships = false);
    Task<List<User>> GetAllAsync();
}