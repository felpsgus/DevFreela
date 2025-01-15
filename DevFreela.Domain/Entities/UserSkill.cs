using DevFreela.Domain.Shared;

namespace DevFreela.Domain.Entities;

public class UserSkill : Entity
{
    public UserSkill(long userId, long skillId)
    {
        UserId = userId;
        SkillId = skillId;
    }

    public long UserId { get; private set; }
    public long SkillId { get; private set; }
}