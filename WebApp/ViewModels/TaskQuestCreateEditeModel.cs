using Core.Resources.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Activity = Core.BLL.DTO.Entities.Activity;

namespace WebApp.ViewModels;

public class TaskQuestCreateEditeModel
{
    public SelectList? Types { get; set; }
    public SelectList? Activities { get; set; }
    public Core.BLL.DTO.Entities.TaskQuest TaskQuest { get; set; } = default!;
}