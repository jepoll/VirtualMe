namespace WebApp.DTO;

public class ItemCreateInfo
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsConsumable { get; set; } = default!;
    public int? StatToUpgrade { get; set; } = default!;
    public int ItemRarity { get; set; } = default!;
    public int? Slot { get; set; } = default!;
    public int Price { get; set; } = default!;
    public string? Image { get; set; } = default!;
    public string? Object { get; set; } = default!;
}