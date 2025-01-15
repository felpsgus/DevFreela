using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(long Id) : ICommand<Result>;