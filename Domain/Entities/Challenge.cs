namespace Domain.Entities;
public class Challenge
{
    public Guid Id { get; private set; }
    public User? User { get; private set; }
    public Guid UserId { get; private set; }
    public long Seconds { get; private set; }
    public long? Improvement { get; private set; }
    public string Comment { get; private set; }
    public DateTimeOffset DateOfExecution { get; private set; }

    public Challenge()
    {
        // for efcore
    }

    public Challenge(Guid userId, long seconds, string comment)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Seconds = seconds;
        Comment = comment;
        DateOfExecution = DateTimeOffset.Now;
    }

    public Challenge(Guid userId, long seconds, string comment, long? improvement)
        : this(userId, seconds, comment)
    {
        Improvement = improvement;
    }

    public void UpdateSeconds(long newSeconds, string comment)
    {
        Comment = comment;
        Seconds = newSeconds;
    }

    public void UpdateSeconds(long newSeconds, string comment, long improvement)
    {
        Comment = comment;
        Seconds = newSeconds;
        Improvement = improvement;
    }
}
