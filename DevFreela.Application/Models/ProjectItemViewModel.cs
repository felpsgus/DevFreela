using DevFreela.Domain.Entities;

namespace DevFreela.Application.Models;

public class ProjectItemViewModel
{
    public ProjectItemViewModel(long id, string title, string clientName, string freelancerName)
    {
    }

    public long Id { get; set; }
    public string Title { get; set; }
    public string ClientName { get; set; }
    public string FreelancerName { get; set; }

    public static ProjectItemViewModel FromEntity(Project project)
    {
        return new ProjectItemViewModel(project.Id, project.Title, project.Client.FullName, project.Freelancer.FullName);
    }
}