using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;

namespace DevFreela.Application.Views;

public class UserViewModel
{
    public UserViewModel(long id, string name, string email, DateOnly birthDate, RoleEnum[] roles,
        List<SkillItemViewModel> skills, List<ProjectItemViewModel> ownedProjects,
        List<ProjectItemViewModel> freelanceProjects)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Roles = roles;
        Skills = skills;
        OwnedProjects = ownedProjects;
        FreelanceProjects = freelanceProjects;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public RoleEnum[] Roles { get; set; }

    public List<SkillItemViewModel> Skills { get; set; }
    public List<ProjectItemViewModel> OwnedProjects { get; set; }
    public List<ProjectItemViewModel> FreelanceProjects { get; set; }

    public static UserViewModel FromEntity(User user)
    {
        return new UserViewModel(
            user.Id,
            user.FullName,
            user.Email,
            user.BirthDate,
            user.Roles,
            user.Skills.Select(SkillItemViewModel.FromEntity).ToList(),
            user.OwnedProjects.Select(ProjectItemViewModel.FromEntity).ToList(),
            user.FreelanceProjects.Select(ProjectItemViewModel.FromEntity).ToList()
        );
    }
}