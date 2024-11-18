using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<ResultViewModel<UserViewModel>>
{
    public GetUserByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}