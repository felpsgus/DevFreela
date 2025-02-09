using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;
using DevFreela.Domain.Enums;

namespace DevFreela.Application.Users.Commands.InsertUser;

public record InsertUserCommand(
    string Name,
    string Email,
    DateOnly BirthDate,
    string Password,
    RoleEnum[] Roles,
    HashSet<long>? Skills)
    : ICommand<Result<long>>;