using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<Result<UserViewModel>>
{
    public GetUserByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}