using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.InsertComment;

public record InsertCommentCommand : ICommand<Result>
{
    [Required] public string Content { get; set; }
    [Required] public long IdProject { get; set; }
    [Required] public long IdUser { get; set; }
}