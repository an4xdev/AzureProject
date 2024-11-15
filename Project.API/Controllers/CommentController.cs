using Microsoft.AspNetCore.Mvc;
using Project.API.Services.Comment;
using Project.Shared.Requests;
using Project.Shared.Responses;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController(ICommentService commentService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<int> GetCommentsByPost(Guid id)
    {
        return await Task.FromResult(1);
    }

    [HttpPost]
    public async Task<BaseResponse> AddComment(AddCommentRequest request)
    {
        var response = await commentService.AddComment(request);
        return await Task.FromResult(response);
    }
}