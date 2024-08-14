namespace Core.DTO.v1_0.Entities;

public class Logs 
{
    public Guid AvatarId { get; set; } = default!;
    public Avatar? Avatar { get; set; } = default!;
    
    public DateTime Time { get; set; } = default!;
    
    public string Message { get; set; } = default!;

    public Guid Id { get; set; }
}