using System.ComponentModel.DataAnnotations;

namespace Project.API.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}