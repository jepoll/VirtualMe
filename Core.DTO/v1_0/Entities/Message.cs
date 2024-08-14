namespace Core.DTO.v1_0.Entities;

public class Message 
{
    public Guid ChatId { get; set; } = default!;
    public Chat? Chat { get; set; } = default!;

    public string SenderName { get; set; } = default!;
    
    public string MessageContent { get; set; } = default!;
    
    public DateTime SendingDate { get; set; } = default!;

    public Guid Id { get; set; }
}