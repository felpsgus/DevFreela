using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<ResultViewModel>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TotalCost { get; set; }
}