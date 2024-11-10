using Microsoft.AspNetCore.Mvc;
using Project.API.Database;
using Project.API.Services;
using Project.API.Services.FileService;

namespace Project.API.Controllers;
[ApiController]
[Route("[controller]")]
public class PostController(AppDbContext _context, IFileservice _fileservice, ISendOnTopic _sendOnTopic) : ControllerBase
{
    [HttpPost]
    public async Task<int> AddPost()
    {
        return await Task.FromResult(1);
    }

    [HttpGet]
    public async Task<int> GetAllPosts()
    {
        return await Task.FromResult(1);
    }

    [HttpGet("{id:guid}")]
    public async Task<int> GetUserPosts(Guid id)
    {
        return await Task.FromResult(1);
    }

    [HttpPut]
    public async Task<int> UpdatePost()
    {
        return await Task.FromResult(1);
    }

    [HttpDelete("{id:guid}")]
    public async Task<int> DeletePost(Guid id)
    {
        return await Task.FromResult(1);
    }
}