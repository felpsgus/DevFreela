using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand : ICommand<Result>
{
    public DeleteUserCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}