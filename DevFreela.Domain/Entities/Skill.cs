using DevFreela.Domain.Shared;

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

    public Skill(long id)
    {
        Id = id;
        Description = default!;
    }

    public string Description { get; private set; }

    public void Update(string description)
    {
        Description = description;
    }
}