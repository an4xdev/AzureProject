using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Models;
using Project.API.Services;
using Project.API.Services.FileService;
using Project.Shared.DTOs;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(AppDbContext context, IFileService fileService, IPostService postService,ISendOnTopic _sendOnTopic) : ControllerBase
{
    [HttpPost]
    public async Task<BaseResponse> AddPost(AddPostRequest request)
    {
        BaseResponse response = new();
        var user = await context.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();
        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "No user in system, cannot add new post.";
            return await Task.FromResult(response);
        }

        var path = await fileService.Upload(request.PhotoData, request.PhotoName);
        var post = new Post
        {
            Description = request.Description,
            Likes = 0,
            PhotoPath = path,
            PhotoName = request.PhotoName,
            PhotoExtension = request.PhotoExtension,
            User = user,
            UserId = user.Id
        };

        context.Posts.Add(post);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        return await Task.FromResult(response);
    }

    [HttpGet]
    public async Task<List<PostDto>> GetAllPosts()
    {
        List<PostDto> response = [];

        var posts = await context.Posts.ToListAsync();

        foreach (var p in posts)
        {
            var post = await postService.GetPostById(p.Id);
            if (post != null)
            {
                response.Add(post);
            }
        }

        return await Task.FromResult(response);

    }

    [HttpGet("{userId:guid}")]
    public async Task<List<PostDto>> GetUserPosts(Guid userId)
    {
        List<PostDto> response = [];

        var posts = await context.Posts.Where(p => p.UserId == userId).ToListAsync();

        foreach (var p in posts)
        {
            var post = await postService.GetPostById(p.Id);
            if (post != null)
            {
                response.Add(post);
            }
        }

        return await Task.FromResult(response);
    }

    [HttpPut]
    public async Task<int> UpdatePost()
    {
        return await Task.FromResult(1);
    }

    [HttpDelete("{id:guid}")]
    public async Task<BaseResponse> DeletePost(Guid id)
    {
        BaseResponse response = new();

        var post = await context.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (post == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown post to delete.";
            return await Task.FromResult(response);
        }

        context.Posts.Remove(post);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        return await Task.FromResult(response);
    }
}