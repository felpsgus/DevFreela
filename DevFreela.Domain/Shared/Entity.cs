namespace DevFreela.Domain.Shared;

public abstract class Entity
{
    public long Id { get; set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public bool Deleted { get; private set; } = false;

    public void Delete() => Deleted = true;

    public void Update() => UpdatedAt = DateTimeOffset.UtcNow;
}