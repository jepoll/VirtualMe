using System.ComponentModel.DataAnnotations;
using Core.Domain.Enums;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class Interior : IDomainEntityId
{
    public string Name { get; set; } = default!;
    
    public int Level { get; set; } = default!;
    
    public EStats Stat { get; set; } = default!;
    
    public float Boost { get; set; } = default!;
    
    public int Cost { get; set; } = default!;
    
    public float LevelNeeded { get; set; } = default!;

    public Guid Id { get; set; }
}