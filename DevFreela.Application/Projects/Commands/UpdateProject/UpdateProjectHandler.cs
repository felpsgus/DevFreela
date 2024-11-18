using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
            {
                return ResultViewModel.Error("Project not found");
            }
            project.Update(request.Title, request.Description, request.TotalCost);
            await _projectRepository.UpdateAsync(project);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error("An error occurred while updating the project");
            throw;
        }
    }
}