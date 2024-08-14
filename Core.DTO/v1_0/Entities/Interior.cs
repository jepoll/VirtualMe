using Core.Domain.Enums;

namespace Core.DTO.v1_0.Entities;

public class Interior
{
    public string Name { get; set; } = default!;
    public int Level { get; set; } = default!;
    
    public EStats Stat { get; set; } = default!;
    
    public float Boost { get; set; } = default!;
    
    public int Cost { get; set; } = default!;
    
    public float LevelNeeded { get; set; } = default!;

    public Guid Id { get; set; }
}