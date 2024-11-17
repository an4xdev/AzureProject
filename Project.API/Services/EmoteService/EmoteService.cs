using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Models;
using Project.Shared.DTOs;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Services.EmoteService;

public interface IEmoteService
{
    public Task<BaseResponse> AddEmote(ToggleEmoteToPostRequest request);
    public Task<BaseResponse> RemoveEmote(ToggleEmoteToPostRequest request);
    public Task<List<EmoteDto>> GetEmotesByPostId(Guid postId);
}

public class EmoteService(AppDbContext context) :IEmoteService
{
    public async Task<BaseResponse> AddEmote(ToggleEmoteToPostRequest request)
    {
        BaseResponse response = new();
        var post = await context.Posts.Where(p => p.Id == request.PostId).FirstOrDefaultAsync();
        if (post == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown post.";
            return await Task.FromResult(response);
        }

        var user = await context.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();

        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown user.";
            return await Task.FromResult(response);
        }

        var emote = await context.Emotes.Where(e => e.Id == request.EmoteId).FirstOrDefaultAsync();
        if (emote == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown emote";
            return await Task.FromResult(response);
        }

        var postEmote = new PostEmote()
        {
            EmoteId = emote.Id,
            Emote = emote,
            User = user,
            UserId = user.Id,
            Post = post,
            PostId = post.Id,
        };

        await context.PostEmotes.AddAsync(postEmote);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        return await Task.FromResult(response);
    }

    public async Task<BaseResponse> RemoveEmote(ToggleEmoteToPostRequest request)
    {
        BaseResponse response = new();
        var post = await context.Posts.Where(p => p.Id == request.PostId).FirstOrDefaultAsync();
        if (post == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown post.";
            return await Task.FromResult(response);
        }

        var user = await context.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();

        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown user.";
            return await Task.FromResult(response);
        }

        var emote = await context.Emotes.Where(e => e.Id == request.EmoteId).FirstOrDefaultAsync();
        if (emote == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown emote";
            return await Task.FromResult(response);
        }

        var postEmote = await context.PostEmotes
            .Where(pe => pe.EmoteId == emote.Id && pe.UserId == user.Id && pe.PostId == post.UserId)
            .FirstOrDefaultAsync();

        if (postEmote == null)
        {
            response.IsSuccessful = false;
            response.Message = "No emote.";
            return await Task.FromResult(response);
        }

        context.PostEmotes.Remove(postEmote);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        return await Task.FromResult(response);
    }

    public async Task<List<EmoteDto>> GetEmotesByPostId(Guid postId)
    {
        List<EmoteDto> emotes = [];

        await context.Emotes.ForEachAsync(async void (e) =>
        {
            var count =
                context
                    .PostEmotes
                    .Count(pe => pe.PostId == postId && pe.EmoteId == e.Id);

            var users = await
                context
                    .PostEmotes
                    .Where(pe => pe.EmoteId == e.Id && pe.PostId == postId)
                    .Select(pe => pe.UserId)
                    .ToListAsync();

            emotes.Add(new EmoteDto
            {
                Value = e.Emoji,
                Count = count,
                UserIds = users
            });
        });

        return await Task.FromResult(emotes);
    }
}