using System.ComponentModel.DataAnnotations;
using Core.Domain.Enums;
using Shared.Domain;

namespace Core.Domain.Entities;

public class TaskType : BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.TaskType), Name = nameof(Difficulty))]
    public EDifficulty Difficulty { get; set; }
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.TaskType), Name = nameof(MoneyAndExpBoost))]
    [Range(minimum:0, maximum: int.MaxValue)]
    public int Money { get; set; } = default!;
    [Range(minimum:0, maximum: int.MaxValue)]
    public int Exp { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(DaysToComplete))]
    [Range(minimum: 0, maximum: 31)]
    public int DaysToComplete { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(HoursToComplete))]
    [Range(minimum: 0, maximum: 24)]
    public int HoursToComplete { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(MinutesToComplete))]
    [Range(minimum: 1, maximum: 60)]
    public int MinutesToComplete { get; set; } = default!;

    [Range(minimum: 1, maximum: int.MaxValue)]
    public int RewardPriceLimit { get; set; } = default!;
    
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Goal { get; set; }
}