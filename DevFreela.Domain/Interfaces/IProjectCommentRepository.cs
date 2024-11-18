using DevFreela.Domain.Entities;

namespace DevFreela.Domain.Interfaces;

public interface IProjectCommentRepository
{
    Task<long> AddAsync(ProjectComment projectComment);
}