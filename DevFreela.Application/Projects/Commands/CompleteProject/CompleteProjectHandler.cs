using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.CompleteProject;

public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;

    public CompleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project == null)
        {
            return new Error("Project", "Project not found.");
        }

        await _projectRepository.CompleteAsync(project);
        return Result.Success();
    }
}