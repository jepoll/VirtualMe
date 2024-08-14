using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.Entities;

public class Avatar : IDomainEntityId, IDomainAppUser<AppUser>
{
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(AppUserId))]
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(IsActive))]
    public bool IsActive { get; set; }
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Health))]
    [Range(minimum: 0, maximum: 100)]
    public int Health { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Stamina))]
    [Range(minimum: 0, maximum: 100)]
    public int Stamina { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Hunger))]
    [Range(minimum: 0, maximum: 100)]
    public int Hunger { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Stress))]
    [Range(minimum: 0, maximum: 100)]
    public int Stress { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Strength))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Strength { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Dexterity))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Dexterity { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Intelligence))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Intelligence { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Money))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Money { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Sex))]
    public ESex Sex { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Level))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Level { get; set; } = 1;
    
    [Range(minimum: 0, maximum: int.MaxValue)]
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Exp))]
    public int Exp { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(ExpToLevelUp))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int ExpToLevelUp { get; set; } = 100;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Image))]
    public byte[]? Image { get; set; } = default!;
    
    [NotMapped]
    public IFormFile? UploadedImage { get; set; }

    public DateTime LastChanges { get; set; } = default!;
    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n AppUserId: " + AppUser + "\n IsActive: " + IsActive + "\n Health: " + Health +
               "\n Stamina: " + Stamina + "\n Hunger: " + Hunger + "\n Stress: " + Stress + "\n Strength: " + Strength
               + "\n Dexterity: " + Dexterity + "\n Intelligence: " + Intelligence + "\n Money: " + Money + "\n Exp: " 
               + Exp + "\n ExpToLevelUp: " + ExpToLevelUp + "\n Sex: " + Sex + "\n LastChanges: " + LastChanges;
    }
}