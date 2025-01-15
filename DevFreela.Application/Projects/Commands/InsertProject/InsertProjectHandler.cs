using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public sealed class InsertProjectHandler : IRequestHandler<InsertProjectCommand, Result<long>>
{
    private readonly IProjectRepository _projectRepository;

    public InsertProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<long>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Title, request.Description, request.ClientId, request.FreelancerId,
            request.TotalCost);
        var id = await _projectRepository.AddAsync(project, cancellationToken);

        return id;
    }
}