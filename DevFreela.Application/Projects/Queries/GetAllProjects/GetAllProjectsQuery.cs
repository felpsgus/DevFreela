using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Projects.Queries.GetAllProjects;

public record GetAllProjectsQuery : IRequest<Result<List<ProjectItemViewModel>>>;