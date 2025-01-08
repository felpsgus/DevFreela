using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand : ICommand<Result>
{
    public long? Id { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public decimal TotalCost { get; set; }
}