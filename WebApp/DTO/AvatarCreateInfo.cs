using Core.Domain.Enums;

namespace WebApp.DTO;

public class AvatarCreateInfo
{
    public String Sex { get; set; } = default!;
    public String? UploadedImage { get; set; } = default;
}