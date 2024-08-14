using System.ComponentModel.DataAnnotations;
using Core.DAL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.DAL.DTO.AddressTables;

public class AvatarOwnsInterior : IDomainEntityId
{
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;

    public Guid InteriorId { get; set; } = default!;
    public Interior Interior { get; set; } = default!;

    public DateTime PurchaseDate { get; set; } = default!;

    public Guid Id { get; set; }
}