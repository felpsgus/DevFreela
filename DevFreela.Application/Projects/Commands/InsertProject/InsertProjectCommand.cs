using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public class InsertProjectCommand : IRequest<ResultViewModel<long>>
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int IdClient { get; set; }
    [Required]
    public int IdFreelancer { get; set; }
    [Required]
    public decimal TotalCost { get; set; }
}