using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, Result<long>>
{
    private readonly IProjectRepository _projectRepository;

    public InsertProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<long>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer,
            request.TotalCost);
        var id = await _projectRepository.AddAsync(project);
        return Result<long>.Success(id);
    }
}