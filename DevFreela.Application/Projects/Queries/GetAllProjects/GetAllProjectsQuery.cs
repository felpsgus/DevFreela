using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
{
}