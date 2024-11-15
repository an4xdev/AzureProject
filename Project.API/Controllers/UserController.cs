using Microsoft.AspNetCore.Mvc;
using Project.API.Services.UserService;
using Project.Shared.Requests;
using Project.Shared.Responses;
using RegisterRequest = Project.Shared.Requests.RegisterRequest;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var response = await userService.Login(request);
        return await Task.FromResult(response);
    }

    [HttpPost("register")]
    public async Task<BaseResponse> Register(RegisterRequest request)
    {
        var response = await userService.Register(request);
        return await Task.FromResult(response);
    }
}