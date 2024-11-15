using Microsoft.AspNetCore.Mvc;
using Project.API.Services.EmoteService;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmoteController(IEmoteService emoteService) : ControllerBase
{
    [HttpPost("add")]
    public async Task<BaseResponse> AddEmote(ToggleEmoteToPostRequest request)
    {
        var response = await emoteService.AddEmote(request);

        return await Task.FromResult(response);
    }

    [HttpPost("remove")]
    public async Task<BaseResponse> RemoveEmote(ToggleEmoteToPostRequest request)
    {
        var response = await emoteService.RemoveEmote(request);

        return await Task.FromResult(response);
    }
}