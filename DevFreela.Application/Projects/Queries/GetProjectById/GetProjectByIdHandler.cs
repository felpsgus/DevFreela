using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Queries.GetProjectById;

public sealed class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectViewModel>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectViewModel>> Handle(GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, true, cancellationToken);

        if (project == null)
            return new Error("Project", "Project not found");

        var projectViewModel = ProjectViewModel.FromEntity(project);

        return projectViewModel;
    }
}