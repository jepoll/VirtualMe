using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace Core.Domain.Entities;

public class Message : BaseEntityId
{
    public Guid ChatId { get; set; } = default!;
    public Chat? Chat { get; set; } = default!;

    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Message), Name = nameof(SenderName))]
    public string SenderName { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Message), Name = nameof(MessageContent))]
    [MaxLength(1024)]
    public string MessageContent { get; set; } = default!;
    
    // [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Message), Name = nameof(SendingDate))]
    public DateTime SendingDate { get; set; } = default!;
}