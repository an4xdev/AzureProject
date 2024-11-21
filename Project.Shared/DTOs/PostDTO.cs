namespace Project.Shared.DTOs;

public class PostDto
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string PhotoData { get; set; }
    public int Likes { get; set; }
    public List<CommentDto> Comments { get; set; }
    public List<EmoteDto> Emotes { get; set; }

    public UserDto Creator { get; set; }
}