using Core.BLL.DTO.AddressTables;
using Core.Domain.Entities;
using Avatar = Core.BLL.DTO.Entities.Avatar;

namespace WebApp.ViewModels;

public class AvatarInventoryViewModel
{
    public Avatar Avatar { get; set; }
    public IEnumerable<Owns> Items { get; set; }
}