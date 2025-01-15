using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public sealed class DeleteSkillValidator : AbstractValidator<DeleteSkillCommand>
{
    public DeleteSkillValidator(ISkillRepository skillRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage("Id is required")
            .MustAsync(async (id, cancellationToken) => await skillRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Skill not found");
    }
}