using Core.DTO.v1_0.Entities;

namespace Core.DTO.v1_0.AddressTables;

public class AvatarsTasks 
{
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest TaskQuest { get; set; } = default!;
    
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    public DateTime TimeStart { get; set; } = default!;
    
    public DateTime TimeFinish { get; set; } = default!;

    public Guid Id { get; set; }
}