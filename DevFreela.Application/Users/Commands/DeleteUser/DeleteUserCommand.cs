using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<ResultViewModel>
{
    public DeleteUserCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}