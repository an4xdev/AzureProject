namespace Project.Shared.DTOs;

public class EmoteDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public UserDto User { get; set; }
}