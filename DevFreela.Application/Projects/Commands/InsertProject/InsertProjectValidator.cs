using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public sealed class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
{
    public InsertProjectValidator(IUserRepository userRepository)
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MinimumLength(5)
            .WithMessage("Title must have at least 5 characters")
            .MaximumLength(30)
            .WithMessage("Title must have a maximum of 30 characters");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MinimumLength(5)
            .WithMessage("Description must have at least 5 characters")
            .MaximumLength(255)
            .WithMessage("Description must have a maximum of 255 characters");

        RuleFor(p => p.TotalCost)
            .GreaterThan(0)
            .WithMessage("Total cost must be greater than 0");

        RuleFor(p => p.ClientId)
            .NotNull()
            .MustAsync(async (clientId, cancellationToken) =>
                await userRepository.ExistsAsync(clientId, cancellationToken))
            .WithMessage("Client not found");

        RuleFor(p => p.FreelancerId)
            .NotNull()
            .MustAsync(async (freelancerId, cancellationToken) =>
                await userRepository.ExistsAsync(freelancerId, cancellationToken))
            .WithMessage("Freelancer not found");
    }
}