using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            return new Error("User", "User does not exist.");
        }

        await _userRepository.DeleteAsync(user);
        return Result.Success();
    }
}