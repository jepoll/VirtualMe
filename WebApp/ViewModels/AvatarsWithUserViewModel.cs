using Core.BLL.DTO.Entities;
using Core.Domain.Identity;

namespace WebApp.ViewModels;

public class AvatarsWithUserViewModel
{
    public List<Avatar>? Avatars { get; set; } = new();
    public List<AppUser>? AppUsers { get; set; } = new();
}