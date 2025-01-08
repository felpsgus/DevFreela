using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.StartProject;

public record StartProjectCommand : ICommand<Result>
{
    public StartProjectCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}