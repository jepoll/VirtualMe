using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskType = Core.BLL.DTO.Entities.TaskType;

namespace WebApp.Areas.Admin.ViewModels;

public class TaskTypeCreateEditeViewModel
{
    public TaskType TaskType { get; set; } = default!;
    public SelectList? Difficulties { get; set; } = default!;
}