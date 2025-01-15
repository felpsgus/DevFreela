using DevFreela.Domain.Enums;
using DevFreela.Domain.Shared;

namespace DevFreela.Domain.Entities;

public class Project : Entity
{
    protected Project()
    {
    }

    public Project(string title, string description, int clientId, int freelancerId, decimal totalCost)
    {
        Title = title;
        Description = description;
        ClientId = clientId;
        FreelancerId = freelancerId;
        TotalCost = totalCost;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public long ClientId { get; private set; }
    public User? Client { get; private set; }
    public long FreelancerId { get; private set; }
    public User? Freelancer { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; } = ProjectStatusEnum.Created;
    public List<ProjectComment> Comments { get; private set; } = [];

    public void Cancel()
    {
        if (Status is not (ProjectStatusEnum.InProgress or ProjectStatusEnum.Suspended)) return;
        Status = ProjectStatusEnum.Cancelled;
    }

    public void Start()
    {
        if (Status != ProjectStatusEnum.Created) return;
        Status = ProjectStatusEnum.InProgress;
        StartedAt = DateTimeOffset.UtcNow;
    }

    public void Complete()
    {
        if (Status is not (ProjectStatusEnum.PaymentPending or ProjectStatusEnum.InProgress)) return;
        Status = ProjectStatusEnum.Completed;
        CompletedAt = DateTimeOffset.UtcNow;
    }

    public void SetPaymentPending()
    {
        if (Status != ProjectStatusEnum.InProgress) return;
        Status = ProjectStatusEnum.PaymentPending;
    }

    public void Update(string title, string description, decimal totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }

    public void UpdateFreelancer(int FreelancerId)
    {
        FreelancerId = FreelancerId;
    }

    public void AddComment(ProjectComment projectComment)
    {
        Comments.Add(projectComment);
    }
}