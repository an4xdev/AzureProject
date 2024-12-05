using Microsoft.EntityFrameworkCore;
using Project.API.Models;
using Project.API.Services.Utils;

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

        List<Guid> emotes =
        [
            Guid.Parse("6fa459ea-ee8a-3ca4-894e-db77e160355e"),
            Guid.Parse("d9428888-122b-11e1-b85c-61cd3cbb3210"),
            Guid.Parse("e6f8e7d7-3323-4c93-8b85-083fb172a2a0"),
            Guid.Parse("b85fc1ea-a338-11d8-8f73-0002440126c0"),
            Guid.Parse("2f48ff5f-2b47-48d5-a939-dc57a5bcabfb"),
        ];


        modelBuilder.Entity<Emote>().HasData(
            new Emote { Id = emotes[0], Emoji = "❤️" },
            new Emote { Id = emotes[1], Emoji = "🤣" },
            new Emote { Id = emotes[2], Emoji = "😮" },
            new Emote { Id = emotes[3], Emoji = "😭" },
            new Emote { Id = emotes[4], Emoji = "😡" }
        );

        List<Guid> users =
        [
            Guid.Parse("49dc72ab-50ce-49b4-91f6-9c69d5c6e8bb"),
            Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
            Guid.Parse("a70f1ae1-285d-41e3-a93a-1b0dcf0bc44d"),
            Guid.Parse("c9da05f8-892b-41e5-99cc-7a89b6edc789"),
            Guid.Parse("f18b62db-69b3-45b6-9d93-23489db44c4f"),
        ];

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = users[0], Email = "aa@example.com", Name = "aa", Password = SimpleHasher.HashPassword("aa")
            },
            new User
            {
                Id = users[1], Email = "bb@example.com", Name = "bb", Password = SimpleHasher.HashPassword("bb")
            },
            new User
            {
                Id = users[2], Email = "cc@example.com", Name = "cc", Password = SimpleHasher.HashPassword("cc")
            },
            new User
            {
                Id = users[3], Email = "dd@example.com", Name = "dd", Password = SimpleHasher.HashPassword("dd")
            },
            new User
            {
                Id = users[4], Email = "ee@example.com", Name = "ee", Password = SimpleHasher.HashPassword("ee")
            }
        );

        List<Guid> posts =
        [
            Guid.Parse("d2a7e8f6-e26f-489a-832c-4c93b7be62f5"),
            Guid.Parse("d511a30e-e1e5-402f-8c5e-d1e0b5d4f34a"),
            Guid.Parse("9f8fdbcc-3765-4623-91e5-fb4ccf2d78a8"),
            Guid.Parse("3e052e9d-1c18-40c6-8913-94a067b6b5e3"),
            Guid.Parse("0f37c9f4-b3c3-4da4-a7f3-08d6bcd27dcf"),
        ];

        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = posts[0], Description = null, Likes = 50,
                PhotoPath = "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png",
                UserId = users[0]
            },
            new Post
            {
                Id = posts[1], Description = "Some description", Likes = 20,
                PhotoPath = "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png",
                UserId = users[1]
            },
            new Post
            {
                Id = posts[2], Description = "Some other description", Likes = 25,
                PhotoPath = "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png",
                UserId = users[1]
            },
            new Post
            {
                Id = posts[3], Description = "Skibidi", Likes = 30,
                PhotoPath = "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png",
                UserId = users[4]
            },
            new Post
            {
                Id = posts[4], Description = null, Likes = 40,
                PhotoPath = "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png",
                UserId = users[2]
            }
        );

        List<DateTime> times =
        [
            new(2024, 10, 1, 12, 0, 12),
            new(2024, 11, 15, 8, 30, 45),
            new(2024, 9, 10, 17, 15, 23),
            new(2024, 11, 27, 23, 59, 59),
            new(2024, 12, 1, 14, 34, 44)
        ];

        List<Guid> comments =
        [
            Guid.Parse("a2f1a7c8-3f1b-4db9-b251-ffbb924fcd47"),
            Guid.Parse("c3d5e77e-d60a-4219-a5b7-869f6d2e03b2"),
            Guid.Parse("e5a2a814-98a0-4d9d-9c2e-04f32a58f2bd"),
            Guid.Parse("f7bc2d1c-0f5c-4b25-a5e8-08c9df3f6f3b"),
            Guid.Parse("b1e9873d-5036-4e21-9ea2-0f74b2bcb3db")
        ];

        modelBuilder.Entity<Comment>().HasData(
            new Comment { Id = comments[0], PostId = posts[0], Time = times[0], UserId = users[1], Value = "Great!" },
            new Comment { Id = comments[1], PostId = posts[0], Time = times[1], UserId = users[4], Value = "LOL!" },
            new Comment { Id = comments[2], PostId = posts[1], Time = times[2], UserId = users[0], Value = "KEKW!" },
            new Comment { Id = comments[3], PostId = posts[2], Time = times[3], UserId = users[3], Value = "Slay!" },
            new Comment { Id = comments[4], PostId = posts[2], Time = times[4], UserId = users[4], Value = "Skibidi!" }
        );

        List<Guid> postEmotes =
        [
            Guid.Parse("bfb5e5ac-1a6d-4c69-9094-1cd627e38a0a"),
            Guid.Parse("e31d39a7-f3d1-4b75-8dc1-abc62c3bb9d5"),
            Guid.Parse("d02a51f5-4ee1-4a9b-a3b5-d76b3e3a7716"),
            Guid.Parse("c7e8fd21-318c-4e94-bd1d-f6e3743c9a6b"),
            Guid.Parse("94c4b210-5f9f-468c-95a6-c11d37f82a93"),
            Guid.Parse("11e2bffa-7d44-40be-a0a6-bcc32b618289"),
            Guid.Parse("f0537b5e-420f-4577-b348-c44dc543fc09"),
            Guid.Parse("aa2de3f9-d5f0-41eb-95df-f3b08b446fae"),
            Guid.Parse("843d9b36-c24d-43d9-b98f-9cbfcbdc1ae6"),
            Guid.Parse("3b99b4cf-dfb7-4e6c-bb68-88dc96c2dca2")
        ];

        modelBuilder.Entity<PostEmote>().HasData(
            new PostEmote { Id = postEmotes[0], EmoteId = emotes[0], PostId = posts[0], UserId = users[1] },
            new PostEmote { Id = postEmotes[1], EmoteId = emotes[2], PostId = posts[4], UserId = users[3] },
            new PostEmote { Id = postEmotes[2], EmoteId = emotes[4], PostId = posts[0], UserId = users[0] },
            new PostEmote { Id = postEmotes[3], EmoteId = emotes[1], PostId = posts[2], UserId = users[2] },
            new PostEmote { Id = postEmotes[4], EmoteId = emotes[3], PostId = posts[1], UserId = users[4] },
            new PostEmote { Id = postEmotes[5], EmoteId = emotes[0], PostId = posts[3], UserId = users[1] },
            new PostEmote { Id = postEmotes[6], EmoteId = emotes[2], PostId = posts[2], UserId = users[4] },
            new PostEmote { Id = postEmotes[7], EmoteId = emotes[4], PostId = posts[1], UserId = users[3] },
            new PostEmote { Id = postEmotes[8], EmoteId = emotes[1], PostId = posts[0], UserId = users[0] },
            new PostEmote { Id = postEmotes[9], EmoteId = emotes[3], PostId = posts[4], UserId = users[2] }
        );
    }
}