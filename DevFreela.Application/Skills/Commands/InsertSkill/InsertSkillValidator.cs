using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Skills.Commands.InsertSkill;

public sealed class InsertSkillValidator : AbstractValidator<InsertSkillCommand>
{
    public InsertSkillValidator(ISkillRepository skillRepository)
    {
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(30)
            .WithMessage("Maximum length is 30 characters.")
            .MustAsync(async (description, cancellationToken) =>
                await skillRepository.CheckDescriptionAsync(description, cancellationToken) == false)
            .WithMessage("Skill already exists.");
    }
}