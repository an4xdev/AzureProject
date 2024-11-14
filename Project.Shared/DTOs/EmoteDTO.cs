namespace Project.Shared.DTOs;

public class EmoteDto
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public int Count { get; set; }
    public List<Guid> UserIds { get; set; }
}