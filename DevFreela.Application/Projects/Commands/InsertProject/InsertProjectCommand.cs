using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public record InsertProjectCommand(string Title, string Description, int ClientId, int FreelancerId, decimal TotalCost)
    : ICommand<Result<long>>;