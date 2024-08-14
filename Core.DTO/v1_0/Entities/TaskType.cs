using Core.Domain.Enums;

namespace Core.DTO.v1_0.Entities;

public class TaskType
{
    public EDifficulty Difficulty { get; set; }
    
    public float MoneyAndExpBoost { get; set; }
    
    public int DaysToComplete { get; set; } = default!;
    
    public int HoursToComplete { get; set; } = default!;
    
    public int MinutesToComplete { get; set; } = default!;

    public Guid Id { get; set; }
}