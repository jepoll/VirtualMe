using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Shared.Domain;

namespace Core.Domain.AddressTables;

public class Owns: BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar? Avatar { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(ItemId))]
    public Guid ItemId { get; set; } = default!;
    public Item? Item { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(Amount))]
    // [Range(minimum: 0, maximum: 99)]
    public int? Amount { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(IsEquipped))]
    public bool IsEquipped { get; set; } = false;
}