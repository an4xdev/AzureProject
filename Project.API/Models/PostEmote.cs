using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.API.Models;

public class PostEmote
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    public Post Post { get; set; }

    [ForeignKey("Emote")]
    public Guid EmoteId { get; set; }
    public Emote Emote { get; set; }

    public int Count { get; set; }
}