using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertComment;

public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectCommentRepository _projectCommentRepository;
    private readonly IUserRepository _userRepository;

    public InsertCommentHandler(IProjectRepository projectRepository,
        IProjectCommentRepository projectCommentRepository, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _projectCommentRepository = projectCommentRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.IdProject);
        if (project == null)
            return new Error("Project", "Project not found");

        var user = await _userRepository.GetByIdAsync(request.IdUser);
        if (user == null)
            return new Error("User", "User not found");

        var projectComment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
        await _projectCommentRepository.AddAsync(projectComment);

        return Result.Success();
    }
}