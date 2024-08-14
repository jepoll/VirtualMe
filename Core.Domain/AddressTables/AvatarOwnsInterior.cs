using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Shared.Domain;

namespace Core.Domain.AddressTables;

public class AvatarOwnsInterior : BaseEntityId
{
    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarOwnsInterior), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;

    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarOwnsInterior), Name = nameof(InteriorId))]
    public Guid InteriorId { get; set; } = default!;
    public Interior Interior { get; set; } = default!;

    // [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarOwnsInterior), Name = nameof(IsActive))]
    public DateTime PurchaseDate { get; set; } = default!;
}