using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.Shared.DTOs;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Services.Comment;

public interface ICommentService
{
    public Task<AddCommentResponse> AddComment(AddCommentRequest request);
    public Task<List<CommentDto>> GetCommentsByPostId(Guid postId);
}

public class CommentService(AppDbContext context) : ICommentService
{
    public async Task<AddCommentResponse> AddComment(AddCommentRequest request)
    {
        AddCommentResponse response = new();

        var post = await context.Posts.Where(p => p.Id == request.PostId).FirstOrDefaultAsync();

        if (post == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown post.";
            return await Task.FromResult(response);
        }

        var user = await context.Users.Where(u => u.Id == request.SenderId).FirstOrDefaultAsync();

        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown user.";
            return await Task.FromResult(response);
        }

        var now = DateTime.UtcNow;

        var comment = new Models.Comment
        {
            Value = request.CommentData,
            UserId = user.Id,
            Post = post,
            PostId = post.Id,
            Time = now
        };

        await context.Comments.AddAsync(comment);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;
        response.Time = now;

        return await Task.FromResult(response);
    }

    public async Task<List<CommentDto>> GetCommentsByPostId(Guid postId)
    {
        var comments = await context.Comments.Where(c => c.PostId == postId).OrderByDescending(c=>c.Time).Select(c => new CommentDto
        {
            Value = c.Value,
            User = new UserDto
            {
                Id = c.UserId,
            },
            Time = c.Time
        }).ToListAsync();

        return await Task.FromResult(comments);
    }
}