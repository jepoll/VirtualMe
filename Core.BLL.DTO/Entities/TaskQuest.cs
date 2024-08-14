using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.Entities;

public class TaskQuest : IDomainEntityId
{
    public Guid TaskTypeId { get; set; } = default!;
    public TaskType? TaskType { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.TaskQuest), Name = nameof(TaskDescription))]
    [MaxLength(512)]
    public string TaskDescription { get; set; } = default!;
    
    public Guid ActivityId { get; set; } = default!;
    public Activity? Activity { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n TaskDescription: " + TaskDescription + "\n ActivityId: " + ActivityId;
    } 
}