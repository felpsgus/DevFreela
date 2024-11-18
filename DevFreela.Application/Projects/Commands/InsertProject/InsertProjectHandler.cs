using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<long>>
{
    private readonly IProjectRepository _projectRepository;

    public InsertProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel<long>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);
            var id = await _projectRepository.AddAsync(project);
            return ResultViewModel<long>.Success(id);
        }
        catch (Exception e)
        {
            return ResultViewModel<long>.Error("An error occurred while creating the project");
            throw;
        }
    }
}