using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Projects.Commands.CompleteProject;

public sealed class CompleteProjectValidator : AbstractValidator<CompleteProjectCommand>
{
    public CompleteProjectValidator(IProjectRepository projectRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) => await projectRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Project not found.");
    }
}