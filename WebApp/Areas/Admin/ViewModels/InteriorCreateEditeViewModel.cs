using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Interior = Core.BLL.DTO.Entities.Interior;

namespace WebApp.Areas.Admin.ViewModels;

public class InteriorCreateEditeViewModel
{
    public Interior Interior { get; set; } = default!;
    public SelectList? StatList { get; set; } = default!;
}