using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Users.Commands.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator(IUserRepository userRepository)
    {
        RuleFor(u => u.Id)
            .NotNull()
            .WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) => await userRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("User not found.");
    }
}