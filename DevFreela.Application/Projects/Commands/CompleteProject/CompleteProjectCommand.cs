using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.CompleteProject;

public record CompleteProjectCommand : ICommand<Result>
{
    public CompleteProjectCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}