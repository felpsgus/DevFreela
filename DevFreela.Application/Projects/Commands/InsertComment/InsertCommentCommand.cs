using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertComment;

public class InsertCommentCommand : IRequest<ResultViewModel>
{
    public string Content { get; set; }
    public long IdProject { get; set; }
    public long IdUser { get; set; }
}