using Core.DTO.v1_0.Entities;

namespace Core.DTO.v1_0.AddressTables;

public class AvatarOwnsInterior 
{
    public Guid AvatarId { get; set; } = default!;
    public Avatar Avatar { get; set; } = default!;

    public Guid InteriorId { get; set; } = default!;
    public Interior Interior { get; set; } = default!;

    public DateTime PurchaseDate { get; set; } = default!;

    public Guid Id { get; set; }
}