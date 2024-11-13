namespace Project.Shared.Requests;

public class AddCommentRequest
{
    public Guid SenderId { get; set; }
    public string CommentData { get; set; }
}