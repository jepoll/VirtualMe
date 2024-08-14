using System.ComponentModel.DataAnnotations;
using Core.Domain.Enums;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class TaskType : IDomainEntityId
{
    public EDifficulty Difficulty { get; set; }

    public int Money { get; set; } = default!;
    public int Exp { get; set; } = default!;
    
    public int DaysToComplete { get; set; } = default!;
    
    public int HoursToComplete { get; set; } = default!;
    
    public int MinutesToComplete { get; set; } = default!;

    public int Goal { get; set; } = default!;
    
    [Range(minimum: 1, maximum: int.MaxValue)]
    public int RewardPriceLimit { get; set; } = default!;

    public Guid Id { get; set; }
}