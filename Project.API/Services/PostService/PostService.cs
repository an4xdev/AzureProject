using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Models;
using Project.API.Services.FileService;
using Project.Shared.DTOs;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Services.PostService;

public interface IPostService
{
    public Task<PostDto?> GetPostById(Guid postId);
    public Task<List<PostDto>> GetAllPosts();
    public Task<BaseResponse> AddPost(PostRequest request);
    public Task<List<PostDto>> GetUserPosts(Guid userId);
    public Task<BaseResponse> UpdatePost(PostRequest request);
    public Task<BaseResponse> DeletePost(Guid id);
}

public class PostService(AppDbContext context, IFileService fileService, ISendOnTopic sendOnTopic) : IPostService
{
    public async Task<PostDto?> GetPostById(Guid postId)
    {
        var p = await context.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();

        if (p == null)
        {
            return null;
        }

        /* ------------------------------------------------------------------------------------ */
        // TODO: move to service implementation

        var comments = await context.Comments.Where(c => c.PostId == p.Id).Select(c => new CommentDto()
        {
            Value = c.Value,
            User = new UserDto
            {
                Id = c.UserId,
                UserName = c.User.Name
            }
        }).ToListAsync();

        List<EmoteDto> emotes = [];

        await context.Emotes.ForEachAsync(async void (e) =>
        {
            var count =
                context
                    .PostEmotes
                    .Count(pe => pe.PostId == p.Id && pe.EmoteId == e.Id);

            var users = await
                context
                    .PostEmotes
                    .Where(pe => pe.EmoteId == e.Id && pe.PostId == p.Id)
                    .Select(pe => pe.UserId)
                    .ToListAsync();

            emotes.Add(new EmoteDto()
            {
                Value = e.Emoji,
                Count = count,
                UserIds = users
            });
        });

        /* ------------------------------------------------------------------------------------ */

        var photoData = await fileService.Get(p.PhotoName);

        return await Task.FromResult(new PostDto
        {
            Id = p.Id,
            PhotoData = photoData,
            Likes = p.Likes,
            Emotes = emotes,
            Comments = comments,
            Description = p.Description
        });
    }

    public async Task<List<PostDto>> GetAllPosts()
    {
        List<PostDto> response = [];

        var posts = await context.Posts.ToListAsync();

        foreach (var p in posts)
        {
            var post = await GetPostById(p.Id);
            if (post != null)
            {
                response.Add(post);
            }
        }

        return await Task.FromResult(response);
    }

    public async Task<BaseResponse> AddPost(PostRequest request)
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

        // await sendOnTopic.SendMessage("FOO");

        return await Task.FromResult(response);
    }

    public async Task<List<PostDto>> GetUserPosts(Guid userId)
    {
        List<PostDto> response = [];

        var posts = await context.Posts.Where(p => p.UserId == userId).ToListAsync();

        foreach (var p in posts)
        {
            var post = await GetPostById(p.Id);
            if (post != null)
            {
                response.Add(post);
            }
        }

        return await Task.FromResult(response);
    }

    public async Task<BaseResponse> UpdatePost(PostRequest request)
    {
        BaseResponse response = new();

        if (request.Id == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown post to edit, no post specified.";
            return await Task.FromResult(response);
        }

        var post = await context.Posts.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        if (post == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown post to edit";
            return await Task.FromResult(response);
        }

        post.Description = request.Description;

        // TODO: think about photo uploads

        context.Posts.Update(post);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;
        return await Task.FromResult(response);
    }

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