using Core.BLL.DTO.Entities;
using Core.BLL.DTO.AddressTables;

namespace WebApp.ViewModels;

public class ActivityIndexViewModel
{
    public AvatarsActivity? Activity { get; set; } = default!;
    public IEnumerable<Activity> Activities { get; set; } = default!;
}