using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface ISkillRepository
{
    Task<long> AddAsync(Skill user);
    Task UpdateAsync(Skill user);
    Task DeleteAsync(Skill user);
    Task<Skill?> GetByIdAsync(long id);
    Task<List<Skill>> GetAllAsync();
}