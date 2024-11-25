using DevFreela.Application.Models;
using DevFreela.Application.Skills.Commands.InsertSkill;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Commands.InsertSkill;

public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<long>>
{
    private readonly ISkillRepository _skillRepository;

    public InsertSkillHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<ResultViewModel<long>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var skill = new Skill(request.Description);
            var id = await _skillRepository.AddAsync(skill);
            return ResultViewModel<long>.Success(id);
        }
        catch (Exception e)
        {
            return ResultViewModel<long>.Error("An error occurred while creating the skill.");
            throw;
        }
    }
}