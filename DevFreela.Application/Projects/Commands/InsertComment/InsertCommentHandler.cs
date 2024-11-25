using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertComment;

public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
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

    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _projectRepository.GetByIdAsync(request.IdProject);
            if (project == null)
                return ResultViewModel.Error("Project not found");

            var user = await _userRepository.GetByIdAsync(request.IdUser);
            if (user == null)
                return ResultViewModel.Error("User not found");

            var projectComment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
            await _projectCommentRepository.AddAsync(projectComment);

            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error("An error occurred while adding a comment to the project");
            throw;
        }
    }
}