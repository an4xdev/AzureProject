namespace Project.Client.Models;

public class CurrentUser
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"ID: {UserId}, Name: {UserName}, email: {Email}";
    }
}