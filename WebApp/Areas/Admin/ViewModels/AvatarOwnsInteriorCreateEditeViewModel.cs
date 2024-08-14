using Core.Domain.AddressTables;
using Microsoft.AspNetCore.Mvc.Rendering;
using AvatarOwnsInterior = Core.BLL.DTO.AddressTables.AvatarOwnsInterior;

namespace WebApp.Areas.Admin.ViewModels;

public class AvatarOwnsInteriorCreateEditeViewModel
{
    public AvatarOwnsInterior OwnsInterior { get; set; } = default!;

    public SelectList? AvatarId { get; set; } = default!;
    public SelectList? InteriorId { get; set; } = default!;
}