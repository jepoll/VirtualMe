using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.Entities;

public class Avatar : IDomainEntityId, IDomainAppUser<AppUser>
{
    public Guid AppUserId { get; set; }

    public AppUser? AppUser { get; set; }

    public bool IsActive { get; set; }

    public int Health { get; set; } = default!;

    public int Stamina { get; set; } = default!;

    public int Hunger { get; set; } = default!;

    public int Stress { get; set; } = default!;

    public int Strength { get; set; } = default!;

    public int Dexterity { get; set; } = default!;

    public int Intelligence { get; set; } = default!;

    public int Money { get; set; } = default!;

    public ESex Sex { get; set; } = default!;

    public int Level { get; set; } = 1;

    public int Exp { get; set; } = default!;

    public int ExpToLevelUp { get; set; } = 100;

    public byte[]? Image { get; set; } = default!;

    public IFormFile? UploadedImage { get; set; }

    public DateTime LastChanges { get; set; } = default!;
    public Guid Id { get; set; }
}