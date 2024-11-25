using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Skills.Commands.InsertSkill;

public class InsertSkillCommand : IRequest<ResultViewModel<long>>
{
    public string Description { get; set; }
}