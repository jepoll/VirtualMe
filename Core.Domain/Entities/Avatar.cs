using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Shared.Domain;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Shared.Contracts.Domain;

namespace Core.Domain.Entities;

public class Avatar : BaseEntityId, IDomainAppUser<AppUser>
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(AppUserId))]
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(IsActive))]
    public bool IsActive { get; set; }
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Health))]
    [Range(minimum: 0, maximum: 100)]
    public int Health { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Stamina))]
    [Range(minimum: 0, maximum: 100)]
    public int Stamina { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Hunger))]
    [Range(minimum: 0, maximum: 100)]
    public int Hunger { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Stress))]
    [Range(minimum: 0, maximum: 100)]
    public int Stress { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Strength))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Strength { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Dexterity))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Dexterity { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Intelligence))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Intelligence { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Money))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Money { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Sex))]
    public ESex Sex { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Level))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Level { get; set; } = 1;
    
    // [Range(minimum: 0, maximum: int.MaxValue)]
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Exp))]
    public int Exp { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(ExpToLevelUp))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int ExpToLevelUp { get; set; } = 100;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Avatar), Name = nameof(Image))]
    public byte[]? Image { get; set; } = default!;
    
    [NotMapped]
    public IFormFile? UploadedImage { get; set; }

    public DateTime LastChanges { get; set; } = default!;

    public void AddExp(int expToAdd)
    {
        Exp += expToAdd;
        CheckForLevelUp();
    }

    public void CheckForLevelUp()
    {
        if (Exp >= ExpToLevelUp)
        {
            Level++;
            SetNewExpToLevelUp();
            Exp = 0;
        }
    }

    private void SetNewExpToLevelUp()
    {
        var prevExpToLevelUp = ExpToLevelUp;
        ExpToLevelUp = (int) Math.Round(prevExpToLevelUp + prevExpToLevelUp * 1.2);
    }
}