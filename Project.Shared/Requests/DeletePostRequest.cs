namespace Project.Shared.Requests;

public class DeletePostRequest
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}