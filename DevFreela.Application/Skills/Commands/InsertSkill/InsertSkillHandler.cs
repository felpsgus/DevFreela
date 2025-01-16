using DevFreela.Application.Abstractions;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Commands.InsertSkill;

public sealed class InsertSkillHandler : IRequestHandler<InsertSkillCommand, Result<long>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InsertSkillHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill(request.Description);
        await _skillRepository.AddAsync(skill, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return skill.Id;
    }
}