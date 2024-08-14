namespace Core.DTO.v1_0.Entities;

public class TaskQuest 
{
    public Guid TaskTypeId { get; set; } = default!;
    public TaskType? TaskType { get; set; } = default!;
    
    public string TaskDescription { get; set; } = default!;
    
    public int CurrentState { get; set; }
    
    public int GoalState { get; set; }

    public Guid Id { get; set; }
}