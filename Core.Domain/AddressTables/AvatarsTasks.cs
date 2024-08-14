using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Shared.Domain;

namespace Core.Domain.AddressTables;

public class AvatarsTasks : BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(TaskQuestId))]
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest TaskQuest { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(TimeStart))]
    public DateTime TimeStart { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(TimeFinish))]
    // public DateTime TimeFinish { get; set; } = default!;
    
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int CurrentState { get; set; }
}