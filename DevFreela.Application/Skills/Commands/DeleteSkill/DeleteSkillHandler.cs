using DevFreela.Application.Abstractions;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Commands.DeleteSkill;

public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, Result>
{
    private readonly ISkillRepository _skillRepository;

    public DeleteSkillHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<Result> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetByIdAsync(request.Id);
        if (skill == null)
        {
            return new Error("Skill", "Skill does not exist.");
        }

        await _skillRepository.DeleteAsync(skill);
        return Result.Success();
    }
}