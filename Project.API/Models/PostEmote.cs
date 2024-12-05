using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.API.Models;

public class PostEmote
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    public Post Post { get; set; } = default!;

    [ForeignKey("Emote")]
    public Guid EmoteId { get; set; }

    public Emote Emote { get; set; } = default!;

    [ForeignKey("User")]
    public Guid UserId { get; set; }

}