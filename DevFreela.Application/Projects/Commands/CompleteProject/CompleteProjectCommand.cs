using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.CompleteProject;

public record CompleteProjectCommand(long Id) : ICommand<Result>;