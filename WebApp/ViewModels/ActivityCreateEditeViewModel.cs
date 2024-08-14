using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Activity = Core.BLL.DTO.Entities.Activity;

namespace WebApp.ViewModels;

public class ActivityCreateEditeViewModel
{
    public Activity Activity { get; set; } = default!;
    public SelectList? Types { get; set; } = default!;
    public SelectList? Stats { get; set; } = default!;
}