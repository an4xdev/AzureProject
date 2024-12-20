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
            response.Message = "Unknown post. No post in system.";
            return await Task.FromResult(response);
        }

        var user = await context.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();

        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown user. No user in system.";
            return await Task.FromResult(response);
        }

        var emote = await context.Emotes.Where(e => e.Id == request.EmoteId).FirstOrDefaultAsync();
        if (emote == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown emote. No emote in system.";
            return await Task.FromResult(response);
        }

        var postEmote = new PostEmote
        {
            EmoteId = emote.Id,
            Emote = emote,
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
            response.Message = "Unknown post. No post in system.";
            return await Task.FromResult(response);
        }

        var user = await context.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();

        if (user == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown user. No user in system.";
            return await Task.FromResult(response);
        }

        var emote = await context.Emotes.Where(e => e.Id == request.EmoteId).FirstOrDefaultAsync();
        if (emote == null)
        {
            response.IsSuccessful = false;
            response.Message = "Unknown emote. No emote in system.";
            return await Task.FromResult(response);
        }

        var postEmote = await context.PostEmotes
            .Where(pe => pe.EmoteId == emote.Id && pe.UserId == user.Id && pe.PostId == post.Id)
            .FirstOrDefaultAsync();

        if (postEmote == null)
        {
            response.IsSuccessful = false;
            response.Message = "No emote. No correlation in system";
            return await Task.FromResult(response);
        }

        context.PostEmotes.Remove(postEmote);

        await context.SaveChangesAsync();

        response.IsSuccessful = true;

        return await Task.FromResult(response);
    }

    public async Task<List<EmoteDto>> GetEmotesByPostId(Guid postId)
    {
        var emotes = await context.Emotes.ToListAsync();

        var emoteDtos = new List<EmoteDto>();

        foreach (var e in emotes)
        {
            var count = await context.PostEmotes
                .CountAsync(pe => pe.PostId == postId && pe.EmoteId == e.Id);

            var users = await context.PostEmotes
                .Where(pe => pe.EmoteId == e.Id && pe.PostId == postId)
                .Select(pe => pe.UserId)
                .ToListAsync();

            emoteDtos.Add(new EmoteDto
            {
                Id = e.Id,
                Value = e.Emoji,
                Count = count,
                UserIds = users
            });
        }

        return await Task.FromResult(emoteDtos);
    }
}