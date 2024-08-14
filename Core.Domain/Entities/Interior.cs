using System.ComponentModel.DataAnnotations;
using Core.Domain.Enums;
using Shared.Domain;

namespace Core.Domain.Entities;

public class Interior : BaseEntityId
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Level))]
    [Range(minimum: 1, maximum: 3)]
    public int Level { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Stat))]
    public EStats Stat { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Boost))]
    [Range(minimum: 0, maximum: float.MaxValue)]
    public float Boost { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(CostForUpgrade))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Cost { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(LevelNeeded))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public float LevelNeeded { get; set; } = default!;
}