using System.ComponentModel.DataAnnotations;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<ResultViewModel>
{
    public long? Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal TotalCost { get; set; }
}