using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Shared.Domain;

namespace Core.Domain.AddressTables;

public class AvatarsActivity : BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(ActivityTypeId))]
    public Guid ActivityId { get; set; } = default!;
    public Activity Activity { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(TimeFinish))]
    public DateTime TimeFinish { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(Start))]
    public DateTime Start { get; set; } = default!;
}