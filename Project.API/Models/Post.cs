using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.API.Models;

public class Post
{
    [Key]
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public string PhotoPath { get; set; }

    public int Likes { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public User User { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public ICollection<PostEmote> PostEmotes { get; set; } = new List<PostEmote>();
}