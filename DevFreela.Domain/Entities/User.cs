using DevFreela.Domain.Shared;

namespace DevFreela.Domain.Entities;

public class User : Entity
{
    protected User()
    {
    }

    public User(string fullName, string email, DateOnly birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public bool Active { get; private set; } = true;

    public ICollection<Skill> Skills { get; private set; } = [];
    public ICollection<UserSkill> UserSkills { get; init; } = [];
    public ICollection<Project> OwnedProjects { get; private set; } = [];
    public ICollection<Project> FreelanceProjects { get; private set; } = [];
    public ICollection<ProjectComment> Comments { get; private set; } = [];

    public void Update(string requestName, string requestEmail, List<long> userSkills)
    {
        FullName = requestName;
        Email = requestEmail;
        UpdateSkills(userSkills);
    }

    public void Inactivate()
    {
        Active = false;
    }

    public void Activate()
    {
        Active = true;
    }

    public void UpdateSkills(List<long> userSkills)
    {
        var newUserSkills = userSkills.Where(us => UserSkills.All(us1 => us1.SkillId != us)).ToList();
        var skillsToRemove = UserSkills.Where(us => userSkills.All(us1 => us1 != us.SkillId)).ToList();

        foreach (var skill in newUserSkills)
        {
            UserSkills.Add(new UserSkill(Id, skill));
        }

        foreach (var skill in skillsToRemove)
        {
            UserSkills.Remove(skill);
        }
    }
}