using DevFreela.Application.Models;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<GetAllUsersViewModel>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<List<GetAllUsersViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetAllAsync();

            var usersViewModel = users
                .Select(u => new GetAllUsersViewModel(u.Id, u.FullName, u.BirthDate))
                .ToList();

            return ResultViewModel<List<GetAllUsersViewModel>>.Success(usersViewModel);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<GetAllUsersViewModel>>.Error("An error occurred while retrieving users.");
            throw;
        }
    }
}