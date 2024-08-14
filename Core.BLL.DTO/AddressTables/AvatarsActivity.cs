using System.ComponentModel.DataAnnotations;
using Core.BLL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.AddressTables;

public class AvatarsActivity : IDomainEntityId
{
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(ActivityId))]
    public Guid ActivityId { get; set; } = default!;
    public Activity Activity { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(TimeFinish))]
    public DateTime TimeFinish { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsActivity), Name = nameof(Start))]
    public DateTime Start { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n ActivityId: " + ActivityId + "\n AvatarId: " + AvatarId + "\n TimeFinish: " + TimeFinish
            + "\n Start: " + Start;
    }
}