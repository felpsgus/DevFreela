using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Interfaces;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public record DeleteSkillCommand : ICommand<Result>
{
    public DeleteSkillCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}