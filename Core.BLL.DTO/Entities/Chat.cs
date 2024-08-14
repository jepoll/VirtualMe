using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.Entities;

public class Chat : IDomainEntityId
{
    public Guid Avatar1Id { get; set; } = default!;
    public Avatar? Avatar1 { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Chat), Name = nameof(FirstAvatarsName))]
    public string FirstAvatarsName { get; set; } = default!;
    public Guid Avatar2Id { get; set; } = default!;
    public Avatar? Avatar2 { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Chat), Name = nameof(SecondAvatarsName))]
    public string SecondAvatarsName { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n Avatar1Id: " + Avatar1Id + "\n 1AvatarName: " + FirstAvatarsName +
               "\n Avatar2Id: " + Avatar2Id + "\n 2AvatarName: " + SecondAvatarsName;
    }
}