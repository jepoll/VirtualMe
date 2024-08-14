using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO;

public class RegisterInfo
{
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Email { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Password { get; set; } = default!;
    
    // [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    // public string Firstname { get; set; } = default!;
    //
    // [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    // public string Lastname { get; set; } = default!;

    [StringLength(64, MinimumLength = 1, ErrorMessage = "Incorrect length!")]
    public string NickName { get; set; } = default!;

}