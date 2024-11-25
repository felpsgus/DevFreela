using DevFreela.Application.Models;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Queries.GetAllSkills;

public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillItemViewModel>>>
{
    private readonly ISkillRepository _skillRepository;

    public GetAllSkillsHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<ResultViewModel<List<SkillItemViewModel>>> Handle(GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var skills = await _skillRepository.GetAllAsync();

            var skillsViewModel = skills
                .Select(u => new SkillItemViewModel(u.Id, u.Description))
                .ToList();

            return ResultViewModel<List<SkillItemViewModel>>.Success(skillsViewModel);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<SkillItemViewModel>>.Error("An error occurred while retrieving skills.");
            throw;
        }
    }
}