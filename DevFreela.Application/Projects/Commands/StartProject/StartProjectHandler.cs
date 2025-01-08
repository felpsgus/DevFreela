using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.StartProject;

public class StartProjectHandler : IRequestHandler<StartProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;

    public StartProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project == null)
            return new Error("Project", "Project not found");

        await _projectRepository.StartAsync(project);

        return Result.Success();
    }
}