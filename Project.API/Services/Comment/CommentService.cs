using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.Shared.DTOs;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Services.Comment;

public interface ICommentService
{
    public Task<BaseResponse> AddComment(AddCommentRequest request);
    public Task<List<CommentDto>> GetCommentsByPostId(Guid postId);
}

public class CommentService(AppDbContext context) : ICommentService
{
    public async Task<BaseResponse> AddComment(AddCommentRequest request)
    {
        BaseResponse response = new();

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

        var comment = new Models.Comment()
        {
            Value = request.CommentData,
            User = user,
            UserId = user.Id,
            Post = post,
            PostId = post.Id
        };

        await context.Comments.AddAsync(comment);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;
        return await Task.FromResult(response);
    }

    public async Task<List<CommentDto>> GetCommentsByPostId(Guid postId)
    {
        var comments = await context.Comments.Where(c => c.PostId == postId).Select(c => new CommentDto
        {
            Value = c.Value,
            User = new UserDto
            {
                Id = c.UserId,
                UserName = c.User.Name
            }
        }).ToListAsync();

        return await Task.FromResult(comments);
    }
}