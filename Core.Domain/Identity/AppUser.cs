using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Shared.Contracts.Domain;

namespace Core.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    // [MinLength(1)]
    // [MaxLength(64)]
    // public string? FirstName { get; set; } = default!;
    //
    // [MinLength(1)]
    // [MaxLength(64)]
    // public string? LastName { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(64)]
    public string NickName { get; set; } = default!;
    public ICollection<Avatar>? Avatars { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }

}