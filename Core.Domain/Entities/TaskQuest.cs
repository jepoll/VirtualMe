﻿using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace Core.Domain.Entities;

public class TaskQuest : BaseEntityId
{
    public Guid TaskTypeId { get; set; } = default!;
    public TaskType? TaskType { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.TaskQuest), Name = nameof(TaskDescription))]
    [MaxLength(512)]
    public string TaskDescription { get; set; } = default!;

    public Guid ActivityId { get; set; } = default!;
    public Activity? Activity { get; set; } = default!;
}