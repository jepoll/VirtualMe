using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.Entities;

public class Message : IDomainEntityId
{
    public Guid ChatId { get; set; } = default!;
    public Chat? Chat { get; set; } = default!;

    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Message), Name = nameof(SenderName))]
    public string SenderName { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Message), Name = nameof(MessageContent))]
    [MaxLength(1024)]
    public string MessageContent { get; set; } = default!;
    
    [Display(ResourceType = typeof(Core.Resources.Domain.Entities.Message), Name = nameof(SendingDate))]
    public DateTime SendingDate { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id" + Id + "\n SenderName: " + SenderName + "\n MessageContent: " + MessageContent +
               "\n SendingDate: " + SendingDate;
    }
}