using DevFreela.Application.Models;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Queries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id, true);

            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error("User not found.");
            }

            var userViewModel = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(userViewModel);
        }
        catch (Exception e)
        {
            return ResultViewModel<UserViewModel>.Error("An error occurred while retrieving the user.");
            throw;
        }
    }
}