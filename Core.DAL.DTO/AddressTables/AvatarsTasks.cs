using System.ComponentModel.DataAnnotations;
using Core.DAL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.AddressTables;

public class AvatarsTasks : IDomainEntityId
{
    public Guid TaskQuestId { get; set; } = default!;
    public TaskQuest TaskQuest { get; set; } = default!;
    
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    public DateTime TimeStart { get; set; } = default!;
    
    // public DateTime TimeFinish { get; set; } = default!;
    
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int CurrentState { get; set; }

    public Guid Id { get; set; }
}