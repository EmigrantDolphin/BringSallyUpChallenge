namespace Domain.Entities;
public class User 
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string PlainPassword { get; private set; }

    public User()
    {
        // for ef core
    }

    public User(string username, string plainPassword)
    {
        Id = Guid.NewGuid();
        Username = username;
        PlainPassword = plainPassword;
    }
}
