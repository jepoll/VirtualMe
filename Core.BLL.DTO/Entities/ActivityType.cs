using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.Entities;

public class ActivityType : IDomainEntityId
{
    [MaxLength(128)]
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(Name))]
    public string Name { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(DaysToComplete))]
    [Range(minimum: 0, maximum: 31)]
    public int DaysToComplete { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(HoursToComplete))]
    [Range(minimum: 0, maximum: 24)]
    public int HoursToComplete { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(MinutesToComplete))]
    [Range(minimum: 1, maximum: 60)]
    public int MinutesToComplete { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(Exp))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Exp { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.ActivityType), Name = nameof(Value))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Value { get; set; } = default!; // Will be used for avatars stat incrementation
    
    public int Money { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return Id.ToString() + "\n Name: " + Name + "\n Time: " + DaysToComplete + '|' + HoursToComplete + "|" +
               MinutesToComplete
               + "\n Exp: " + Exp + "\n Money: " + Money + "\n Value: " + Value;
    }
}