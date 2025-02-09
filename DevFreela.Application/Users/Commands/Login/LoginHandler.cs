using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;
using DevFreela.Application.Views;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<LoginViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<Result<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var hashPassword = _authService.ComputeHash(request.Password);
        var user = await _userRepository.GetUserByCredentials(request.Email, hashPassword, cancellationToken);

        if (user == null)
            return Result<LoginViewModel>.Error(new Error("User", "User not found"));

        var token = _authService.GenerateJwtToken(user);

        return Result<LoginViewModel>.Success(new LoginViewModel(token));
    }
}