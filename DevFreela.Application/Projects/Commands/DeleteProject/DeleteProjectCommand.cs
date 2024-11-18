using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest<ResultViewModel>
{
    public DeleteProjectCommand(long id)
    {
    }

    public long Id { get; private set; }
}