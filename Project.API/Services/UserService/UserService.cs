using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.Shared.DTOs;
using Project.Shared.Responses;

namespace Project.API.Services.UserService;

public interface IUserService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<BaseResponse> Register(RegisterRequest request);
}

public class UserService(AppDbContext context) : IUserService
{
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

    public async Task<BaseResponse> Register(RegisterRequest request)
    {
        BaseResponse response = new();
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