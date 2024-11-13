﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.API.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    public string Value { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public User User { get; set; }

    [ForeignKey("Post")]
    public Guid PostId { get; set; }

    public Post Post { get; set; }
}