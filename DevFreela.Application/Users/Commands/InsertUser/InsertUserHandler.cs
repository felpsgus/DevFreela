using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.InsertUser;

public sealed class InsertUserHandler : IRequestHandler<InsertUserCommand, Result<long>>
{
    private readonly IUserRepository _userRepository;

    public InsertUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<long>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email, request.BirthDate);
        var id = await _userRepository.AddAsync(user, cancellationToken);
        user.UpdateSkills(request.Skills?.ToList() ?? []);
        await _userRepository.UpdateAsync(user, cancellationToken);
        return id;
    }
}