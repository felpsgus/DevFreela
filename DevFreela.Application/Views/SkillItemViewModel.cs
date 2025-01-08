using DevFreela.Domain.Entities;

namespace DevFreela.Application.Views;

public class SkillItemViewModel
{
    public SkillItemViewModel(long id, string description)
    {
        Id = id;
        Description = description;
    }

    public long Id { get; set; }
    public string Description { get; set; }

    public static SkillItemViewModel FromEntity(Skill skill)
    {
        return new SkillItemViewModel(skill.Id, skill.Description);
    }
}