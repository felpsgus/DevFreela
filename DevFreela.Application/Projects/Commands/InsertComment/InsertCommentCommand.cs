using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertComment;

public class InsertCommentCommand : IRequest<ResultViewModel>
{
    [Required]
    public string Content { get; set; }
    [Required]
    public long IdProject { get; set; }
    [Required]
    public long IdUser { get; set; }
}