using System.ComponentModel.DataAnnotations;
using Core.BLL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.AddressTables;

public class Owns: IDomainEntityId
{
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar? Avatar { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(ItemId))]
    public Guid ItemId { get; set; } = default!;
    public Item? Item { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(Amount))]
    [Range(minimum: 0, maximum: 99)]
    public int? Amount { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Owns), Name = nameof(IsEquipped))]
    public bool IsEquipped { get; set; } = false;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n AvatarId: " + AvatarId + "\n ItemId: " + ItemId + "\n Amount: " + Amount +
               "\n IsEquipped: " + IsEquipped;
    } 
}