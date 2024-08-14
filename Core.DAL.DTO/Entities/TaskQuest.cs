using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class TaskQuest : IDomainEntityId
{
    public Guid TaskTypeId { get; set; } = default!;
    public TaskType? TaskType { get; set; } = default!;
    
    public string TaskDescription { get; set; } = default!;
    
    public Guid ActivityId { get; set; } = default!;
    public Activity? Activity { get; set; } = default!;

    public Guid Id { get; set; }
}