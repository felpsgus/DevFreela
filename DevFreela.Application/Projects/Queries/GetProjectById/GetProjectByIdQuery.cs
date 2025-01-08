using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<Result<ProjectViewModel>>
{
    public GetProjectByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}