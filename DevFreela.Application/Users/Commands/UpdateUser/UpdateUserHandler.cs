using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return ResultViewModel.Error("User does not exist.");
            }

            user.Update(request.Name, request.Email, request.Skills.ToList());
            await _userRepository.UpdateAsync(user);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error("An error occurred while updating the user.");
            throw;
        }
    }
}