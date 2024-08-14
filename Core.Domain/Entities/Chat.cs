using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace Core.Domain.Entities;

public class Chat : BaseEntityId
{
    public Guid Avatar1Id { get; set; } = default!;
    public Avatar? Avatar1 { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Chat), Name = nameof(FirstAvatarsName))]
    public string FirstAvatarsName { get; set; } = default!;
    public Guid Avatar2Id { get; set; } = default!;
    public Avatar? Avatar2 { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Chat), Name = nameof(SecondAvatarsName))]
    public string SecondAvatarsName { get; set; } = default!;
}