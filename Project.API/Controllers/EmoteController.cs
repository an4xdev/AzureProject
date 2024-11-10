using Microsoft.AspNetCore.Mvc;
using Project.API.Database;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmoteController(AppDbContext _context) : ControllerBase
{
    [HttpPost("add")]
    public async Task<int> AddEmote()
    {
        return await Task.FromResult(1);
    }

    [HttpPost("remove")]
    public async Task<int> RemoveEmote()
    {
        return await Task.FromResult(1);
    }
}