using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserViewModel>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserViewModel>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, true, cancellationToken);

        if (user == null)
            return new Error("User", "User not found.");

        var userViewModel = UserViewModel.FromEntity(user);

        return userViewModel;
    }
}