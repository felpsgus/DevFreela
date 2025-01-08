using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync((long)request.Id);
        if (project == null)
        {
            return new Error("Project", "Project not found");
        }

        project.Update(request.Title, request.Description, request.TotalCost);
        await _projectRepository.UpdateAsync(project);
        return Result.Success();
    }
}