﻿using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace Core.Domain.Entities;

public class Logs : BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Logs), Name = nameof(AvatarId))]
    public Guid? AvatarId { get; set; } = default!;
    public Avatar? Avatar { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Resources.Domain.Entities.Logs), Name = "Time")]
    public DateTime Time { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Logs), Name = nameof(Message))]
    public string Message { get; set; } = default!;
}