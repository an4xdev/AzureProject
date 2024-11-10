using Microsoft.EntityFrameworkCore;
using Project.API.Models;

namespace Project.API.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Emote> Emotes { get; set; }
    public DbSet<PostEmote> PostEmotes { get; set; }
}