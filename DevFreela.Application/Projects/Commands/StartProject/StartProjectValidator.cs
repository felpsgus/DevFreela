using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Projects.Commands.StartProject;

public sealed class StartProjectValidator : AbstractValidator<StartProjectCommand>
{
    public StartProjectValidator(IProjectRepository projectRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) => await projectRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Project not found.");
    }
}