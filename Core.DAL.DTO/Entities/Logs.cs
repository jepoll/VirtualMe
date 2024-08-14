using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class Logs : IDomainEntityId
{
    public Guid? AvatarId { get; set; } = default!;
    public Avatar? Avatar { get; set; } = default!;
    
    public DateTime Time { get; set; } = default!;
    
    public string Message { get; set; } = default!;

    public Guid Id { get; set; }
}