using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Shared.Domain;

namespace Core.Domain.AddressTables;

public class Reward : BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Reward), Name = nameof(TaskQuestId))]
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest? TaskQuest { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Reward), Name = nameof(ItemId))]
    public Guid ItemId { get; set; } = default!;
    public Item? Item { get; set; } = default!;
    
    public int Quantity { get; set; } = default!;
}