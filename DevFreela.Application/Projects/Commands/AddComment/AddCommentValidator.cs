using DevFreela.Domain.Interfaces;
using FluentValidation;

namespace DevFreela.Application.Projects.Commands.AddComment;

public sealed class AddCommentValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentValidator(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        RuleFor(p => p.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(255)
            .WithMessage("Maximum length is 255 characters.");

        RuleFor(p => p.ProjectId)
            .NotNull()
            .WithMessage("ProjectId is required.")
            .MustAsync(async (id, cancellationToken) => await projectRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Project not found.");

        RuleFor(p => p.UserId)
            .NotNull()
            .WithMessage("UserId is required.")
            .MustAsync(async (id, cancellationToken) => await userRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("User not found.");
    }
}