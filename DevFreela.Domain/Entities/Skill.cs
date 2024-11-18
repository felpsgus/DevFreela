namespace DevFreela.Domain.Entities;

public class Skill : Entity
{
    protected Skill()
    {
    }

    public Skill(string description)
    {
        Description = description;
    }

    public string Description { get; private set; }
    public List<UserSkill> UserSkills { get; private set; } = [];
}