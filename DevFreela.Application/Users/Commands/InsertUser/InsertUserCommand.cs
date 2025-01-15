using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Users.Commands.InsertUser;

public record InsertUserCommand(string Name, string Email, DateOnly BirthDate, HashSet<long>? Skills)
    : ICommand<Result<long>>;