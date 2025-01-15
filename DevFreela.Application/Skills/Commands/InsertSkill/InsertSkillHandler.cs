using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Commands.InsertSkill;

public sealed class InsertSkillHandler : IRequestHandler<InsertSkillCommand, Result<long>>
{
    private readonly ISkillRepository _skillRepository;

    public InsertSkillHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<Result<long>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill(request.Description);
        var id = await _skillRepository.AddAsync(skill, cancellationToken);
        return id;
    }
}