using DevFreela.Application.Models;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.CompleteProject;

public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public CompleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
            {
                return ResultViewModel.Error("Project not found");
            }

            await _projectRepository.CompleteAsync(project);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error(e.Message);
        }
    }
}