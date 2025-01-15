using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(long Id) : ICommand<Result>;