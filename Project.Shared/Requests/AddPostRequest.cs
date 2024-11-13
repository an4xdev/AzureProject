namespace Project.Shared.Requests;

public class AddPostRequest
{
    public Guid? Id { get; set; }

    public string Description { get; set; }

    public string PhotoData { get; set; }

    public string PhotoName { get; set; }

    public string PhotoExtension { get; set; }

    public Guid UserId { get; set; }
}