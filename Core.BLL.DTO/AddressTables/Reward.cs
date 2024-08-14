using System.ComponentModel.DataAnnotations;
using Core.BLL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.AddressTables;

public class Reward : IDomainEntityId
{
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Reward), Name = nameof(TaskQuestId))]
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest? TaskQuest { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Reward), Name = nameof(ItemId))]
    public Guid ItemId { get; set; } = default!;
    public Item? Item { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.Reward), Name = nameof(Quantity))]
    [Range(minimum: 1, maximum: int.MaxValue)]
    public int Quantity { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n TaskQuestId: " + TaskQuestId + "\n ItemId: " + ItemId + "\n Quantity: " + Quantity;
    }
}