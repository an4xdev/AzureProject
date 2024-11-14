using Microsoft.Azure.Amqp.Serialization;
using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Services.FileService;
using Project.Shared.DTOs;

namespace Project.API.Services;

public interface IPostService
{
    public Task<PostDto?> GetPostById(Guid postId);
}

public class PostService(AppDbContext context, IFileService fileService) : IPostService
{
    public async Task<PostDto?> GetPostById(Guid postId)
    {
        var p = await context.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();

        if (p == null)
        {
            return null;
        }

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
}