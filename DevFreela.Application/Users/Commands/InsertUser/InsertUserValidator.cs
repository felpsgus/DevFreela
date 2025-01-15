using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Users.Commands.InsertUser;

public sealed class InsertUserValidator : AbstractValidator<InsertUserCommand>
{
    public InsertUserValidator(IUserRepository userRepository, ISkillRepository skillRepository)
    {
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
            .MaximumLength(30)
            .WithMessage("Maximum length is 30 characters.")
            .MustAsync(async (email, cancellationToken) => await userRepository.CheckEmailAsync(email, cancellationToken) == false)
            .WithMessage("E-mail already in use.");

        RuleFor(p => p.BirthDate)
            .NotEmpty()
            .WithMessage("Birth date is required.")
            .LessThan(new DateOnly(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day))
            .WithMessage("User must be at least 18 years old.");

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