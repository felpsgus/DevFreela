using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public class DeleteSkillCommand : IRequest<ResultViewModel>
{
    public DeleteSkillCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}