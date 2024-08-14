using System.ComponentModel.DataAnnotations;
using Core.DAL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.AddressTables;

public class Owns: IDomainEntityId
{
    public Guid AvatarId { get; set; } = default!;
    public Avatar? Avatar { get; set; } = default!;
    
    public Guid ItemId { get; set; } = default!;
    public Item? Item { get; set; } = default!;
    
    public int? Amount { get; set; } = default!;
    
    public bool IsEquipped { get; set; } = false;

    public Guid Id { get; set; }
}