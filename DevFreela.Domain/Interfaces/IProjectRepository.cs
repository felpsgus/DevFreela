using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface IProjectRepository
{
    Task<long> AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task StartAsync(Project project);
    Task CompleteAsync(Project project);
    Task DeleteAsync(Project project);
    Task<Project?> GetByIdAsync(long id);
    Task<List<Project>> GetAllAsync();
}