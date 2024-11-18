using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
{
    public GetProjectByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}