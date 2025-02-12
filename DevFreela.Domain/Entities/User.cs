using DevFreela.Domain.Enums;
using DevFreela.Domain.Exceptions;
using DevFreela.Domain.Shared;

namespace DevFreela.Domain.Entities;

public class User : Entity
{
    protected User()
    {
    }

    public User(string fullName, string email, DateOnly birthDate, string password, RoleEnum[] roles)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Password = password;
        Roles = roles;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public bool Active { get; private set; } = true;
    public string Password { get; private set; }
    public RoleEnum[] Roles { get; private set; }

    public ICollection<Skill> Skills { get; private set; } = [];
    public ICollection<UserSkill> UserSkills { get; init; } = [];
    public ICollection<Project> OwnedProjects { get; private set; } = [];
    public ICollection<Project> FreelanceProjects { get; private set; } = [];
    public ICollection<ProjectComment> Comments { get; private set; } = [];

    public void Update(string requestName, string requestEmail, RoleEnum[] roles, List<long> userSkills)
    {
        FullName = requestName;
        Email = requestEmail;
        Roles = roles;
        UpdateSkills(userSkills);
    }

    public void Inactivate()
    {
        if (Active == false)
            throw new DomainException("User is already inactive");

        Active = false;
    }

    public void Activate()
    {
        if (Active)
            throw new DomainException("User is already active");
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