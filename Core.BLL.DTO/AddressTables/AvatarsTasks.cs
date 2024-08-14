using System.ComponentModel.DataAnnotations;
using Core.BLL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.AddressTables;

public class AvatarsTasks : IDomainEntityId
{
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(TaskQuestId))]
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest TaskQuest { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(TimeStart))]
    public DateTime TimeStart { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarsTasks), Name = nameof(TimeFinish))]
    // public DateTime TimeFinish { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.TaskQuest), Name = nameof(CurrentState))]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int CurrentState { get; set; }

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n TaskQuestId: " + TaskQuestId + "\n AvatarId: " + AvatarId + "\n TimeStart: " + TimeStart
            +"\n CurrentState: " + CurrentState;
    }
}