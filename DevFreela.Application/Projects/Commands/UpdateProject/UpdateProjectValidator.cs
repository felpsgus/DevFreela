using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Projects.Commands.UpdateProject;

public sealed class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectValidator(IProjectRepository projectRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage("Id is required")
            .MustAsync(async (id, cancellationToken) => await projectRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Project not found");

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MinimumLength(5)
            .WithMessage("Title must have a minimum of 5 characters")
            .MaximumLength(30)
            .WithMessage("Title must have a maximum of 30 characters");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MinimumLength(5)
            .WithMessage("Description must have a minimum of 5 characters")
            .MaximumLength(255)
            .WithMessage("Description must have a maximum of 255 characters");

        RuleFor(p => p.TotalCost)
            .GreaterThan(0)
            .WithMessage("Total Cost must be greater than 0");
    }
}