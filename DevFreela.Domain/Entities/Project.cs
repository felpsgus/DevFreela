using DevFreela.Domain.Enums;

namespace DevFreela.Domain.Entities;

public class Project : Entity
{
    protected Project()
    {
    }

    public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public long IdClient { get; private set; }
    public User? Client { get; private set; }
    public long IdFreelancer { get; private set; }
    public User? Freelancer { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
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
        StartedAt = DateTime.Now;
    }

    public void Complete()
    {
        if (Status is not (ProjectStatusEnum.PaymentPending or ProjectStatusEnum.InProgress)) return;
        Status = ProjectStatusEnum.Completed;
        CompletedAt = DateTime.Now;
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
}