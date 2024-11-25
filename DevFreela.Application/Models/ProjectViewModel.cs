using DevFreela.Domain.Entities;

namespace DevFreela.Application.Models;

public class ProjectViewModel
{
    public ProjectViewModel(long id,
        string title,
        string description,
        string clientName,
        string freelancerName,
        decimal totalCost,
        DateTimeOffset? startedAt,
        DateTimeOffset? finishedAt, List<ProjectCommentViewModel> comments)
    {
        Id = id;
        Title = title;
        Description = description;
        ClientName = clientName;
        FreelancerName = freelancerName;
        TotalCost = totalCost;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
        Comments = comments;
    }

    public long Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ClientName { get; private set; }
    public string FreelancerName { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? FinishedAt { get; private set; }

    public List<ProjectCommentViewModel> Comments { get; set; }

    public static ProjectViewModel FromEntity(Project project)
    {
        return new ProjectViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.Client.FullName,
            project.Freelancer.FullName,
            project.TotalCost,
            project.StartedAt,
            project.CompletedAt,
            project.Comments
                .Select(pc => new ProjectCommentViewModel(pc.Content, pc.User.FullName))
                .ToList()
        );
    }
}