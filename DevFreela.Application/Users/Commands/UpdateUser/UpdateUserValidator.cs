using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator(IUserRepository userRepository, ISkillRepository skillRepository)
    {
        RuleFor(u => u.Id)
            .NotNull()
            .WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) => await userRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("User not found.");

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(30)
            .WithMessage("Maximum length is 30 characters.");

        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage("Invalid e-mail.")
            .NotEmpty()
            .WithMessage("E-mail is required.")
            .MustAsync(async (command, email, cancellationToken) =>
            {
                var user = await userRepository.CheckEmailAsync(email, cancellationToken);
                return user == null || user.Id == command.Id;
            })
            .WithMessage("E-mail already in use.");

        When(p => p.Skills != null, () =>
        {
            RuleFor(p => p.Skills)
                .CustomAsync(async (skills, context, cancellationToken) =>
                {
                    foreach (var skillId in skills)
                    {
                        if (!await skillRepository.ExistsAsync(skillId, cancellationToken))
                            context.AddFailure("Skill", $"Skill with id {skillId} does not exist.");
                    }
                });
        });
    }
}