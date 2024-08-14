using Microsoft.AspNetCore.Mvc.Rendering;
using Reward = Core.BLL.DTO.AddressTables.Reward;

namespace WebApp.ViewModels;

public class RewardCreateEditeViewModel
{
    public Reward Reward { get; set; } = default!;
    public SelectList? TaskQuests { get; set; } = default!;
    public SelectList? Items { get; set; } = default!;
}