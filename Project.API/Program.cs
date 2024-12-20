using Microsoft.AspNetCore.Mvc;
using Project.API.Database;
using Project.API.Services.Comment;
using Project.API.Services.EmoteService;
using Project.API.Services.FileService;
using Project.API.Services.PostService;
using Project.API.Services.Topic;
using Project.API.Services.UserService;
using Project.Shared.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSqlServer<AppDbContext>(connectionString);

var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendAppPolicy",
        policy =>
        {
            policy
                .WithOrigins(frontendUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IFileService, AzureFileService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IEmoteService, EmoteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontendAppPolicy");

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Append("Access-Control-Allow-Origin", frontendUrl);
        context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
        context.Response.StatusCode = 200;
        return;
    }
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// USER
app.MapPost("/user/login", async ([FromServices] IUserService userService, [FromBody] LoginRequest request) =>
{
    var response = await userService.Login(request);
    return Results.Ok(response);
});

app.MapPost("/user/register", async ([FromServices] IUserService userService, [FromBody] RegisterRequest request) =>
{
    var response = await userService.Register(request);
    return Results.Ok(response);
});

// POSTS
app.MapPost("/post", async ([FromServices] IPostService postService, [FromBody] PostRequest request) =>
{
    var response = await postService.AddPost(request);
    return Results.Ok(response);
});

app.MapGet("/post", async ([FromServices] IPostService postService) =>
{
    var response = await postService.GetAllPosts();
    return Results.Ok(response);
});

app.MapGet("/post/{userId:guid}", async ([FromServices] IPostService postService, Guid userId) =>
{
    var response = await postService.GetUserPosts(userId);
    return Results.Ok(response);
});

app.MapPut("/post", async ([FromServices] IPostService postService, [FromBody] PostRequest request) =>
{
    var response = await postService.UpdatePost(request);
    return Results.Ok(response);
});

app.MapPut("/post/delete", async ([FromServices] IPostService postService, [FromBody] DeletePostRequest request) =>
{
    var response = await postService.DeletePost(request);
    return Results.Ok(response);
});

// COMMENTS
app.MapPost("/comment", async ([FromServices] ICommentService commentService, [FromBody] AddCommentRequest request) =>
{
    var response = await commentService.AddComment(request);
    return Results.Ok(response);
});

// EMOTES
app.MapPost("/emote/add", async ([FromServices] IEmoteService emoteService, [FromBody] ToggleEmoteToPostRequest request) =>
{
    var response = await emoteService.AddEmote(request);
    return Results.Ok(response);
});

app.MapPost("/emote/remove", async ([FromServices] IEmoteService emoteService, [FromBody] ToggleEmoteToPostRequest request) =>
{
    var response = await emoteService.RemoveEmote(request);
    return Results.Ok(response);
});

app.Run();