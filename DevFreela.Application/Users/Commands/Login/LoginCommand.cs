using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using MediatR;

namespace DevFreela.Application.Users.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginViewModel>>;