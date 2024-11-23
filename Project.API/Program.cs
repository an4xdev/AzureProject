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

builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClientPolicy", build =>
        build.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = null;
});

builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IFileService, LocalFileService>();
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

app.UseCors("BlazorClientPolicy");

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//
//     var context = services.GetRequiredService<AppDbContext>();
//     if (context.Database.GetPendingMigrations().Any())
//     {
//         context.Database.Migrate();
//     }
// }

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

app.MapDelete("/post", async ([FromServices] IPostService postService, [FromBody] DeletePostRequest request) =>
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