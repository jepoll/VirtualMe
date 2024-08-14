using Core.DTO.v1_0.Entities;

namespace Core.DTO.v1_0.AddressTables;

public class Owns
{
    public Guid AvatarId { get; set; } = default!;
    public Avatar? Avatar { get; set; } = default!;
    
    public Guid ItemId { get; set; } = default!;
    public Item? Item { get; set; } = default!;
    
    public int? Amount { get; set; } = default!;
    
    public bool IsEquipped { get; set; } = false;

    public Guid Id { get; set; }
}