using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserItemViewModel>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<UserItemViewModel>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        var usersViewModel = users
            .Select(u => new UserItemViewModel(u.Id, u.FullName))
            .ToList();

        return Result<List<UserItemViewModel>>.Success(usersViewModel);
    }
}