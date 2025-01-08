using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public record InsertProjectCommand : ICommand<Result<long>>
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int IdClient { get; set; }
    [Required] public int IdFreelancer { get; set; }
    [Required] public decimal TotalCost { get; set; }
}