using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.AddComment;

public sealed class AddCommentHandler : IRequestHandler<AddCommentCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCommentHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken: cancellationToken);
        var projectComment = new ProjectComment(request.Content, request.UserId, request.ProjectId);
        project.Comments.Add(projectComment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}