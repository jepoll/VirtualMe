using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class Chat : IDomainEntityId
{
    public Guid Avatar1Id { get; set; } = default!;
    public Avatar? Avatar1 { get; set; } = default!;
    
    public string FirstAvatarsName { get; set; } = default!;
    public Guid Avatar2Id { get; set; } = default!;
    public Avatar? Avatar2 { get; set; } = default!;
    
    public string SecondAvatarsName { get; set; } = default!;

    public Guid Id { get; set; }
}