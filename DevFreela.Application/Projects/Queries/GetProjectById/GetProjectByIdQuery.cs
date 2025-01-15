using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(long Id) : IRequest<Result<ProjectViewModel>>;