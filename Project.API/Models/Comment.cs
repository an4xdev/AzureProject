using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.API.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; }

    public string Value { get; set; }

    public DateTime Time { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public User User = default!;

    [ForeignKey("Post")]
    public Guid PostId { get; set; }

    public Post Post { get; set; } = default!;
}