using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class Message : IDomainEntityId
{
    public Guid ChatId { get; set; } = default!;
    public Chat? Chat { get; set; } = default!;

    public string SenderName { get; set; } = default!;
    
    public string MessageContent { get; set; } = default!;
    
    public DateTime SendingDate { get; set; } = default!;

    public Guid Id { get; set; }
}