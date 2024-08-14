using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Shared.Domain;

namespace Core.Domain.Entities;

public class Item: BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(Name))]
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(Description))]
    [MaxLength(256)]
    public string Description { get; set; } = default!;

    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(IsConsumable))]
    public bool IsConsumable { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(StatToUpgrade))]
    public EStats? StatToUpgrade { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(ItemRarity))]
    public ERarity ItemRarity { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(Slot))]
    public ESlot? Slot { get; set; } = default!;
    
    public int Price { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(Image))]
    public byte[]? Image { get; set; } = default!;
    [NotMapped]
    public IFormFile? UploadedImage { get; set; }
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Item), Name = nameof(ObjectModel))]
    public byte[]? Object { get; set; } = default!;
    [NotMapped]
    public IFormFile? ObjectModel { get; set; } = default!;
}