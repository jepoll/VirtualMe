using System.ComponentModel.DataAnnotations;
using Core.Domain.Enums;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.Entities;

public class Activity : IDomainEntityId
{
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(ActivityTypeId))]
    public Guid ActivityTypeId { get; set; } = default!;
    public ActivityType? ActivityType { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(Stat))]
    public EStats Stat { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(StrengthLimit))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int StrengthLimit { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(DexterityLimit))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int DexterityLimit { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(IntelligenceLimit))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int IntelligenceLimit { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(LevelLimit))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int LevelLimit { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(StressGain))]
    [Range(minimum: -100, maximum: 100)]
    public int StressGain { get; set; } = default!; 
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(StaminaDrain))]
    [Range(minimum: 0, maximum: 100)]
    public int StaminaDrain { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(Name))]
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Activity), Name = nameof(Description))]
    [MaxLength(256)]
    public string Description { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + this.Id + "\n Name: " + Name + "\n Description: " + Description + "\nStat: " + this.Stat +
               "\n StrLimit: " + StrengthLimit + "\n DexLimit: " +
               DexterityLimit + "\n IntLimit: " + IntelligenceLimit + "LvlLimit: " + LevelLimit + "\n StressGain: " + StressGain
               + "\n StaminaDrain: " + StaminaDrain;
    }
}