using Microsoft.AspNetCore.Identity;

namespace WebApp.Helpers;


public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new IdentityError()
    {
        Code = nameof(DefaultError),
        Description = Shared.Resources.Identity.DefaultError
    };
}
