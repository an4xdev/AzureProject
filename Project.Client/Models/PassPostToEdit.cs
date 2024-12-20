namespace Project.Client.Models;

public class PassPostToEdit
{
    public Guid Id { get; set; }

    public string PhotoName { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;
}