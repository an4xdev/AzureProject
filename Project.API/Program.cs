using Project.API.Database;
using Project.API.Services;
using Project.API.Services.Comment;
using Project.API.Services.EmoteService;
using Project.API.Services.FileService;
using Project.API.Services.PostService;
using Project.API.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClientPolicy", build => build.AllowAnyOrigin());
});

builder.Services.AddSingleton<ISendOnTopic, SendOnTopic>();
builder.Services.AddScoped<IFileService, LocalFileService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IEmoteService, EmoteService>();

var app = builder.Build();

// TODO: move to minimal API?

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("BlazorClientPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();