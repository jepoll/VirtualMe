using System.ComponentModel.DataAnnotations;
using Core.Domain.Enums;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class Activity : IDomainEntityId
{
    public Guid ActivityTypeId { get; set; } = default!;
    public ActivityType? ActivityType { get; set; } = default!;
    
    public EStats Stat { get; set; } = default!;
    
    public int StrengthLimit { get; set; } = default!;
    
    public int DexterityLimit { get; set; } = default!;
    
    public int IntelligenceLimit { get; set; } = default!;
    
    public int LevelLimit { get; set; } = default!;
    
    public int StressGain { get; set; } = default!; 
    
    public int StaminaDrain { get; set; } = default!;
    
    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;

    public Guid Id { get; set; }
}