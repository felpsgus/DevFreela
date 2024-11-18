using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.CompleteProject;

public class CompleteProjectCommand : IRequest<ResultViewModel>
{
    public CompleteProjectCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}