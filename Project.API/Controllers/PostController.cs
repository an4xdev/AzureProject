using Microsoft.AspNetCore.Mvc;
using Project.API.Services.PostService;
using Project.Shared.DTOs;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(
    IPostService postService) : ControllerBase
{
    [HttpPost]
    public async Task<BaseResponse> AddPost(PostRequest request)
    {
        var response = await postService.AddPost(request);

        return await Task.FromResult(response);
    }

    [HttpGet]
    public async Task<List<PostDto>> GetAllPosts()
    {
        var response = await postService.GetAllPosts();

        return await Task.FromResult(response);
    }

    [HttpGet("{userId:guid}")]
    public async Task<List<PostDto>> GetUserPosts(Guid userId)
    {
        var response = await postService.GetUserPosts(userId);

        return await Task.FromResult(response);
    }

    [HttpPut]
    public async Task<BaseResponse> UpdatePost(PostRequest request)
    {
        var response = await postService.UpdatePost(request);
        return await Task.FromResult(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<BaseResponse> DeletePost(Guid id)
    {
        var response = await postService.DeletePost(id);

        return await Task.FromResult(response);
    }
}