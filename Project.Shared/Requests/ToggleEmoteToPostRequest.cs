namespace Project.Shared.Requests;

public class ToggleEmoteToPostRequest
{
    public Guid EmoteId { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}