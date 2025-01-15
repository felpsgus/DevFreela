using DevFreela.Application.Abstractions;
using DevFreela.Application.Views;
using DevFreela.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.Skills.Queries.GetAllSkills;

public sealed class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, Result<List<SkillItemViewModel>>>
{
    private readonly ISkillRepository _skillRepository;

    public GetAllSkillsHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<Result<List<SkillItemViewModel>>> Handle(GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        var skills = await _skillRepository.GetAllAsync(cancellationToken);

        var skillsViewModel = skills
            .Select(u => new SkillItemViewModel(u.Id, u.Description))
            .ToList();

        return skillsViewModel;
    }
}