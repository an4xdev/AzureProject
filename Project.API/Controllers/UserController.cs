using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Services.FileService;
using Project.Shared.Responses;
using RegisterRequest = Project.Shared.DTOs.RegisterRequest;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(AppDbContext context) : ControllerBase
{
    [HttpPost("login")]
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        LoginResponse response = new();
        var user = await context.Users.Where(u => u.Email == request.Email &&  u.Password == request.Password)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "No user in system with that credentials";
            return await Task.FromResult(response);

        }
        response.IsSuccessful = true;
        response.UserId = user.Id;
        return await Task.FromResult(response);
    }

    [HttpPost("register")]
    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        RegisterResponse response = new();
        var user = await context.Users.Where(u => u.Email == request.Email).FirstOrDefaultAsync();
        if (user != null)
        {
            response.IsSuccessful = false;
            response.Message = "In system already exists user with provided email.";
            return await Task.FromResult(response);
        }

        response.IsSuccessful = true;
        return await Task.FromResult(response);
    }
}