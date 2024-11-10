using Microsoft.AspNetCore.Mvc;
using Project.API.Database;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController(AppDbContext _context) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<int> GetCommentsByPost(Guid id)
    {
        return await Task.FromResult(1);
    }

    [HttpPost]
    public async Task<int> AddComment()
    {
        return await Task.FromResult(1);
    }
}