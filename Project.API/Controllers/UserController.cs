using Microsoft.AspNetCore.Mvc;
using Project.API.Database;
using Project.API.Services.FileService;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(AppDbContext _context, IFileservice _fileservice) : ControllerBase
{
    [HttpPost("login")]
    public async Task<int> Login()
    {
        return await Task.FromResult(1);
    }

    [HttpPost("register")]
    public async Task<int> Register()
    {
        return await Task.FromResult(1);
    }
}