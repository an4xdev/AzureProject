using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Services.Comment;

public interface ICommentService
{
    public Task<int> GetCommentsByPost(Guid id);
    public Task<BaseResponse> AddComment(AddCommentRequest request);
}

public class CommentService(AppDbContext context) : ICommentService
{
    public async Task<int> GetCommentsByPost(Guid id)
    {
        throw new NotImplementedException();
    }

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
}