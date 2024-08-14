using System.ComponentModel.DataAnnotations;
using Core.DAL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.AddressTables;

public class Reward : IDomainEntityId
{
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest? TaskQuest { get; set; } = default!;
    
    public Guid ItemId { get; set; } = default!;
    public Item? Item { get; set; } = default!;
    public int Quantity { get; set; } = default!;

    public Guid Id { get; set; }
}