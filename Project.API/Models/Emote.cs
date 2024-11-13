using System.ComponentModel.DataAnnotations;

namespace Project.API.Models;

public class Emote
{
    [Key]
    public Guid Id { get; set; }

    public required string Emoji { get; set; }
}