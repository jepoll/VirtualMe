using System.ComponentModel.DataAnnotations;
using Core.BLL.DTO.Entities;
using Shared.Contracts.Domain;

namespace Core.BLL.DTO.AddressTables;

public class AvatarOwnsInterior : IDomainEntityId
{
    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarOwnsInterior), Name = nameof(AvatarId))]
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;

    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarOwnsInterior), Name = nameof(InteriorId))]
    public Guid InteriorId { get; set; } = default!;
    public Interior Interior { get; set; } = default!;

    [Display(ResourceType = typeof(Core.Resources.Domain.AddressTables.AvatarOwnsInterior), Name = nameof(PurchaseDate))]
    public DateTime PurchaseDate { get; set; } = default!;

    public Guid Id { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n AvatarId: " + AvatarId + "\n InteriorId: " + InteriorId + "\n PurchaseDate: " +
               PurchaseDate;
    }
}