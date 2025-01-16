using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertComment;

public sealed class InsertCommentHandler : IRequestHandler<InsertCommentCommand, Result>
{
    private readonly IProjectCommentRepository _projectCommentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InsertCommentHandler(IProjectCommentRepository projectCommentRepository, IUnitOfWork unitOfWork)
    {
        _projectCommentRepository = projectCommentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var projectComment = new ProjectComment(request.Content, request.ProjectId, request.UserId);
        await _projectCommentRepository.AddAsync(projectComment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}