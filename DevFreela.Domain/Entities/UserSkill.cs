namespace DevFreela.Domain.Entities;

public class UserSkill : Entity
{
    protected UserSkill()
    {
    }

    public UserSkill(long idUser, long idSkill)
    {
        IdUser = idUser;
        IdSkill = idSkill;
    }

    public long IdUser { get; private set; }
    public User? User { get; private set; }
    public long IdSkill { get; private set; }
    public Skill? Skill { get; private set; }
}