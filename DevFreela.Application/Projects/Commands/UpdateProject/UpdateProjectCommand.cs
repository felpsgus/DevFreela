using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : ICommand<Result>
{
    [JsonIgnore] public long Id { get; set; }
    [Required] public string Title { get; init; }
    [Required] public string Description { get; init; }
    [Required] public decimal TotalCost { get; init; }
}