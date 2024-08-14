using System.ComponentModel.DataAnnotations;
using Core.DAL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.AddressTables;

public class AvatarsActivity : IDomainEntityId
{
    public Guid ActivityId { get; set; } = default!;
    public Activity Activity { get; set; } = default!;
    
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;
    
    public DateTime TimeFinish { get; set; } = default!;
    
    public DateTime Start { get; set; } = default!;

    public Guid Id { get; set; }
}