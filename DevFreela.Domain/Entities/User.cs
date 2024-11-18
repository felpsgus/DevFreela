namespace DevFreela.Domain.Entities;

public class User : Entity
{
    protected User()
    {
    }

    public User(string fullName, string email, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool Active { get; private set; } = true;

    public List<UserSkill> Skills { get; private set; } = [];
    public List<Project> OwnedProjects { get; private set; } = [];
    public List<Project> FreelanceProjects { get; private set; } = [];
    public List<ProjectComment> Comments { get; private set; } = [];

    public void Update(string requestName, string requestEmail)
    {
        FullName = requestName;
        Email = requestEmail;
    }
}