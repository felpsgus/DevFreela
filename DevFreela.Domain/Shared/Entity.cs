namespace DevFreela.Domain.Entities;

public abstract class Entity
{
    public long Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public bool Deleted { get; private set; } = true;

    public void Delete() => Deleted = true;
}