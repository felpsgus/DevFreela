using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.StartProject;

public class StartProjectCommand : IRequest<ResultViewModel>
{
    public StartProjectCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}