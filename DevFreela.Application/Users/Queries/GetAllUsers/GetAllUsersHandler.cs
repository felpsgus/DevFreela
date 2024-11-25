using DevFreela.Application.Models;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserItemViewModel>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<List<UserItemViewModel>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetAllAsync();

            var usersViewModel = users
                .Select(u => new UserItemViewModel(u.Id, u.FullName))
                .ToList();

            return ResultViewModel<List<UserItemViewModel>>.Success(usersViewModel);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<UserItemViewModel>>.Error("An error occurred while retrieving users.");
            throw;
        }
    }
}