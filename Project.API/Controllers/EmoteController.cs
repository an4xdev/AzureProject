using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Models;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmoteController(AppDbContext context) : ControllerBase
{
    [HttpPost("add")]
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

    [HttpPost("remove")]
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
}