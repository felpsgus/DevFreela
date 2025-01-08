using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            return new Error("User", "User does not exist.");
        }

        user.Update(request.Name, request.Email, request.Skills.ToList());
        await _userRepository.UpdateAsync(user);
        return Result.Success();
    }
}