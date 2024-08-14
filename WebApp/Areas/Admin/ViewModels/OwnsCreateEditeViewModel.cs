using Core.Domain.AddressTables;
using Microsoft.AspNetCore.Mvc.Rendering;
using Owns = Core.BLL.DTO.AddressTables.Owns;

namespace WebApp.Areas.Admin.ViewModels;

public class OwnsCreateEditeViewModel
{
    public Owns Owns { get; set; } = default!;
    public SelectList? Avatars { get; set; } = default!;
    public SelectList? Items { get; set; } = default!;
}