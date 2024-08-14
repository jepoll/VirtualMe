using Core.DTO.v1_0.Entities;

namespace Core.DTO.v1_0.AddressTables;

public class AvatarsActivity 
{
    public Guid ActivityTypeId { get; set; } = default!;
    public Activity Activity { get; set; } = default!;
    
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    public DateTime TimeFinish { get; set; } = default!;
    
    public DateTime Start { get; set; } = default!;

    public Guid Id { get; set; }
}