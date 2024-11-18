using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Projects.Commands.InsertProject;

public class InsertProjectCommand : IRequest<ResultViewModel<long>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
}