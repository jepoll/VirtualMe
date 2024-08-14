using System.ComponentModel.DataAnnotations;
using Core.Domain.Enums;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.Entities;

public class Interior : IDomainEntityId
{
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Name))]
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Level))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Level { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Stat))]
    public EStats Stat { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Boost))]
    [Range(minimum: 0, maximum: float.MaxValue)]
    public float Boost { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(Cost))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Cost { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Interior), Name = nameof(LevelNeeded))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public float LevelNeeded { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n Name: " + Name + "\n Level: " + Level + "\n Stat: " + Stat + "\n Boost: " + Boost
            + "\n Cost: " + Cost + "\n LevelNeeded: " + LevelNeeded;
    }
}