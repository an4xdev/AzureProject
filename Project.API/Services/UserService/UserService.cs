using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Models;
using Project.API.Services.Topic;
using Project.API.Services.Utils;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Services.UserService;

public interface IUserService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<BaseResponse> Register(RegisterRequest request);
}

public class UserService(AppDbContext context, ITopicService topicService) : IUserService
{
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        LoginResponse response = new();
        var user = await context.Users.Where(u => u.Email == request.Email && u.Password == SimpleHasher.HashPassword(request.Password))
            .FirstOrDefaultAsync();
        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "No user in system with that credentials";
            return await Task.FromResult(response);
        }

        response.IsSuccessful = true;
        response.UserId = user.Id;
        response.UserName = user.Name;

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

        var newUser = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = SimpleHasher.HashPassword(request.Password)
        };

        await context.Users.AddAsync(newUser);

        await context.SaveChangesAsync();

        await topicService.CreateSubscription(request.Email);

        response.IsSuccessful = true;
        return await Task.FromResult(response);
    }
}