namespace WebApp.DTO;

public class OwnsCreateInfo
{
    public string AvatarId { get; set; } = default!;
    public string ItemId { get; set; } = default!;
    public int? Amount { get; set; } = default!;
    public bool IsEquiped { get; set; } = default!;
}