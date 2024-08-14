using Core.BLL;
using Core.Contracts.BLL;
using Core.Domain.AddressTables;
using Core.Domain.Entities;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.BLL;
using WebApp.ViewModels;
using Activity = System.Diagnostics.Activity;
using Avatar = Core.BLL.DTO.Entities.Avatar;

namespace WebApp.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICoreBLL _bll;
    private readonly IWebHostEnvironment _env;
    private readonly UserManager<AppUser> _userManager;
    // private readonly IBLLMapper<Core.BLL.DTO.Entities.Avatar, Core.Domain.Entities.Avatar> _mapperAvatar;
    // private readonly IBLLMapper<Core.BLL.DTO.AddressTables.Owns, Core.Domain.AddressTables.Owns> _mapperOwns;
    // private readonly IBLLMapper<Core.BLL.DTO.Entities.Item, Core.Domain.Entities.Item> _mapperItem;

    public HomeController(ILogger<HomeController> logger, ICoreBLL bll, IWebHostEnvironment env,
        UserManager<AppUser> userManager)
    {
        _logger = logger;
        _bll = bll;
        _env = env;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            var avatar = await _bll.Avatar.GetAvatarUpdatedByUserId(user.Id);
            // var avatar = user.Avatars!.First(a => a.IsActive);
            
            if (avatar != null)
            {
                try
                {
                    string path = Path.Combine(_env.WebRootPath, "3DModels", "temp");
            
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    else
                    {
                        Directory.Delete(path, true);
                        Directory.CreateDirectory(path);
                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while cleaning up temp files: {Message}", ex.Message);
                }
                
                var avatarInventory = new AvatarInventoryViewModel
                {
                    Avatar = avatar,
                    Items = (await _bll.Owns.GetAllByAvatarIdAsync(avatar.Id))
                };
                
                foreach (var own in avatarInventory.Items)
                {
                    string filePath = "wwwroot/3DModels/temp/" + own.Item.Name + ".glb";
                    if (!own.IsEquipped)
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        continue;
                    }

                    if (own.Item!.Object == null) continue;
                    if (own.Item.Object.Length == 0) continue;

                    if (!System.IO.File.Exists(filePath))
                    {
                        await System.IO.File.WriteAllBytesAsync(filePath, own.Item.Object!);
                    }
                }

                // avatar.IsActive = true; //Error in other case?
                _bll.Avatar.Update(avatar);
                await _bll.SaveChangesAsync();

                return View(avatarInventory);
            }
            
        }
        // else if (user == null) return View();
        
        return RedirectToAction("Create", "Avatar");

    }

    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(
                new RequestCulture(culture)),
            new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });
        return LocalRedirect(returnUrl);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}