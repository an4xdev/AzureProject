using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Models;
using Project.API.Services.Comment;
using Project.API.Services.EmoteService;
using Project.API.Services.FileService;
using Project.API.Services.Topic;
using Project.Shared.DTOs;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Services.PostService;

public interface IPostService
{
    public Task<PostDto?> GetPostById(Guid postId);
    public Task<List<PostDto>> GetAllPosts();
    public Task<BaseResponse> AddPost(PostRequest request);
    public Task<List<SimplePostDto>> GetUserPosts(Guid userId);
    public Task<BaseResponse> UpdatePost(PostRequest request);
    public Task<BaseResponse> DeletePost(DeletePostRequest request);
}

public class PostService(
    AppDbContext context,
    ICommentService commentService,
    IEmoteService emoteService,
    IFileService fileService,
    ITopicService topicService) : IPostService
{
    public async Task<PostDto?> GetPostById(Guid postId)
    {
        var p = await context.Posts.Where(p => p.Id == postId).Include(post => post.User).FirstOrDefaultAsync();

        if (p == null)
        {
            return null;
        }

        var comments = await commentService.GetCommentsByPostId(p.Id);

        var emotes = await emoteService.GetEmotesByPostId(p.Id);

        return await Task.FromResult(new PostDto
        {
            Id = p.Id,
            PhotoData = p.PhotoPath,
            Likes = p.Likes,
            Emotes = emotes,
            Comments = comments,
            Description = p.Description,
            Creator = new UserDto
            {
                Id = p.User.Id,
                UserName = p.User.Name
            }
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

        var path = await fileService.Upload(request.PhotoData, request.PhotoExtension);
        var post = new Post
        {
            Description = request.Description,
            Likes = 0,
            PhotoPath = path,
            User = user,
            UserId = user.Id
        };

        context.Posts.Add(post);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        await topicService.SendMessage($"{user.Email} just added new image. You must check it now!!!");

        return await Task.FromResult(response);
    }

    public async Task<List<SimplePostDto>> GetUserPosts(Guid userId)
    {
        List<SimplePostDto> response = [];

        var posts = await context.Posts.Where(p => p.UserId == userId).ToListAsync();

        response.AddRange(posts.Select(post =>
            new SimplePostDto
            {
                Id = post.Id, Description = post.Description, PhotoData = post.PhotoPath
            }));

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
            response.Message = "Unknown post to edit. No post in system.";
            return await Task.FromResult(response);
        }

        post.Description = request.Description;

        if (request.IsPhotoChanged)
        {
            await fileService.Replace(post.PhotoPath.Split("/").ToList().LastOrDefault(), request.PhotoData);
        }

        context.Posts.Update(post);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;
        return await Task.FromResult(response);
    }

    public async Task<BaseResponse> DeletePost(DeletePostRequest request)
    {
        BaseResponse response = new();

        var post = await context.Posts.Where(p => p.Id == request.PostId).FirstOrDefaultAsync();

        if (post == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown post to delete. No post in system.";
            return await Task.FromResult(response);
        }

        if (post.UserId != request.UserId)
        {
            response.IsSuccessful = false;
            response.Message = "You can't delete someone posts.";
            return await Task.FromResult(response);
        }

        context.Posts.Remove(post);

        await fileService.Delete(post.PhotoPath);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        return await Task.FromResult(response);
    }
}