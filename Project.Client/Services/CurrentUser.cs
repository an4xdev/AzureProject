namespace Project.Client.Services;

public interface ICurrentUser
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class CurrentUser : ICurrentUser
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}