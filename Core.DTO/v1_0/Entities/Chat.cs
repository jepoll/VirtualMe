namespace Core.DTO.v1_0.Entities;

public class Chat 
{
    public Guid Avatar1Id { get; set; } = default!;
    public Avatar? Avatar1 { get; set; } = default!;
    
    public string FirstAvatarsName { get; set; } = default!;
    public Guid Avatar2Id { get; set; } = default!;
    public Avatar? Avatar2 { get; set; } = default!;
    
    public string SecondAvatarsName { get; set; } = default!;

    public Guid Id { get; set; }
}