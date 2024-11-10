using System.ComponentModel.DataAnnotations;

namespace Project.API.Models;

public class Emote
{
    [Key]
    public int Id { get; set; }

    public required string Emoji { get; set; }
}