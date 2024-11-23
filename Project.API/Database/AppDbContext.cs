using Microsoft.EntityFrameworkCore;
using Project.API.Models;

namespace Project.API.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Emote> Emotes { get; set; }
    public DbSet<PostEmote> PostEmotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Emote>().HasData(
            new Emote { Id = Guid.NewGuid(), Emoji = "❤️" },
            new Emote { Id = Guid.NewGuid(), Emoji = "🤣" },
            new Emote { Id = Guid.NewGuid(), Emoji = "😮" },
            new Emote { Id = Guid.NewGuid(), Emoji = "😭" },
            new Emote { Id = Guid.NewGuid(), Emoji = "😡" }
        );

    }
}