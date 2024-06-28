namespace MSP.Courses.Domain.Entities;

public abstract class MongoEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
    public bool Active { get; protected set; } = true;

    public void SetActive(bool active) => Active = active;
    public void UpdatedAtNow() => UpdatedAt = DateTime.UtcNow;
}