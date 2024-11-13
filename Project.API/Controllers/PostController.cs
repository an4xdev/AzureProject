using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
public class PostController(AppDbContext context, IFileService fileService, ISendOnTopic _sendOnTopic) : ControllerBase
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

        var emotes = await context.Emotes.ToListAsync();

        emotes.ForEach(e =>
        {
            context.PostEmotes.Add(new PostEmote()
            {
                Count = 0,
                Emote = e,
                EmoteId = e.Id,
                Post = post,
                PostId = post.Id
            });
        });

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        return await Task.FromResult(response);
    }

    [HttpGet]
    public async Task<List<PostDto>> GetAllPosts()
    {
        List<PostDto> response = [];

        var posts = await context.Posts.ToListAsync();

        posts.ForEach(Action);

        return await Task.FromResult(response);

        async void Action(Post p)
        {
            var comments = await context.Comments.Where(c => c.PostId == p.Id).Select(c =>new CommentDto()
            {
                Value = c.Value,
                User = new UserDto()
                {
                    Id = c.UserId,
                    UserName = c.User.Name
                }
            }).ToListAsync();

            // TODO: think about specific emote and user corealtion
            var emotes = await context.PostEmotes.Where(pe => pe.PostId == p.Id).Select(pe => new EmoteDto()
            {
                Id = pe.EmoteId,
                // User = pe.
            }).ToListAsync();

            var pDto = new PostDto()
            {
                Id = p.Id,
                // TODO: get photo data from service
                PhotoData = "",
                Likes = p.Likes,
            };
        }
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