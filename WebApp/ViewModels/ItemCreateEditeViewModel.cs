using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Item = Core.BLL.DTO.Entities.Item;

namespace WebApp.ViewModels;

public class ItemCreateEditeViewModel
{
    public Item Item { get; set; } = default!;
    public SelectList? Rarities { get; set; } = default!;
    public SelectList? Stats { get; set; } = default!;
    public SelectList? Slots { get; set; } = default!;
    //public IFormFile? UploadedImage { get; set; } = default!;
}