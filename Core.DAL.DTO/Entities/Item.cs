using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class Item: IDomainEntityId
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool IsConsumable { get; set; } = default!;
    
    public EStats? StatToUpgrade { get; set; } = default!;
    
    public ERarity ItemRarity { get; set; } = default!;
    
    public ESlot? Slot { get; set; } = default!;

    public int Price { get; set; } = default!;
    
    public byte[]? Image { get; set; } = default!;
    public IFormFile? UploadedImage { get; set; }
    
    public byte[]? Object { get; set; } = default!;
    public IFormFile? ObjectModel { get; set; } = default!;

    public Guid Id { get; set; }
}