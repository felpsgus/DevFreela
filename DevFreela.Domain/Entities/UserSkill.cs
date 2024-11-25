using DevFreela.Domain.Shared;

namespace DevFreela.Domain.Entities;

public class UserSkill : Entity
{
    public UserSkill(long idUser, long idSkill)
    {
        IdUser = idUser;
        IdSkill = idSkill;
    }

    public long IdUser { get; private set; }
    public long IdSkill { get; private set; }
}