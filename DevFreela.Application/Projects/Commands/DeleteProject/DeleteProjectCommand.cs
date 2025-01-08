using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand : ICommand<Result>
{
    public DeleteProjectCommand(long id)
    {
    }

    public long Id { get; private set; }
}