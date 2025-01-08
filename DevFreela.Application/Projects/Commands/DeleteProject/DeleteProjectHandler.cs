using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project == null)
        {
            return new Error("Project", "Project not found");
        }

        await _projectRepository.DeleteAsync(project);
        return Result.Success();
    }
}