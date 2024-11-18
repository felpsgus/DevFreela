using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.InsertUser;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<long>>
{
    private readonly IUserRepository _userRepository;

    public InsertUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<long>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = new User(request.Name, request.Email, request.BirthDate);
            var id = await _userRepository.AddAsync(user);
            return ResultViewModel<long>.Success(id);
        }
        catch (Exception e)
        {
            return ResultViewModel<long>.Error("An error occurred while creating the user.");
            throw;
        }
    }
}