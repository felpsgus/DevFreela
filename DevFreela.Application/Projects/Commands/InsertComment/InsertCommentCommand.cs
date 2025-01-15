using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.InsertComment;

public class InsertCommentCommand : ICommand<Result>
{
    [Required] public string Content { get; init; }
    [Required] public long UserId { get; init; }
    [JsonIgnore] public long ProjectId { get; set; }
}