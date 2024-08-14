using Core.DTO.v1_0.Entities;

namespace Core.DTO.v1_0.AddressTables;

public class Reward 
{
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest TaskQuest { get; set; } = default!;
    
    public Guid ItemId { get; set; } = default!;
    public Item Item { get; set; } = default!;
    
    public int MoneyAmount { get; set; } = default!;

    public int ExperienceAmount { get; set; } = default!;

    public Guid Id { get; set; }
}