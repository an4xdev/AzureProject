namespace Project.Shared.DTOs;

public class CommentDto
{
    public string Value { get; set; }
    public UserDto User { get; set; }

    public DateTime Time { get; set; }
}