using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class ActivityType : IDomainEntityId
{
    public string Name { get; set; } = default!;
    
    public int DaysToComplete { get; set; } = default!;
    
    public int HoursToComplete { get; set; } = default!;
    
    public int MinutesToComplete { get; set; } = default!;
    
    public int Exp { get; set; } = default!;
    
    public int Value { get; set; } = default!; // Will be used for avatars stat incrementation

    public int Money { get; set; } = default!;
    
    public Guid Id { get; set; }
}