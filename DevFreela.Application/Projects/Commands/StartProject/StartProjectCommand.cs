using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.StartProject;

public record StartProjectCommand(long Id) : ICommand<Result>;