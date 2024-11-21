namespace Project.Shared.Responses;

public class LoginResponse : BaseResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}