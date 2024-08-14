namespace Core.DTO.v1_0.Entities;

public class ActivityType 
{
    public string Name { get; set; } = default!;
    
    public int DaysToComplete { get; set; } = default!;
    
    public int HoursToComplete { get; set; } = default!;
    
    public int MinutesToComplete { get; set; } = default!;
    
    public int Exp { get; set; } = default!;
    
    public int Value { get; set; } = default!; // Will be used for avatars stat incrementation

    public Guid Id { get; set; }
}