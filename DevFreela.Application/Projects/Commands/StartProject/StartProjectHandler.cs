using DevFreela.Application.Models;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.StartProject;

public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public StartProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
            {
                return ResultViewModel.Error("Project not found");
            }

            await _projectRepository.StartAsync(project);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error(e.Message);
            throw;
        }
    }
}