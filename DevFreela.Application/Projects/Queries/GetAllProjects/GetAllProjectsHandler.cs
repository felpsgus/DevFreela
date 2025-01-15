using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Queries.GetAllProjects;

public sealed class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, Result<List<ProjectItemViewModel>>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);

        var projectsViewModel = projects
            .Select(ProjectItemViewModel.FromEntity)
            .ToList();

        return projectsViewModel;
    }
}