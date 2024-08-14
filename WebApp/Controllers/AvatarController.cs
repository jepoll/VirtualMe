using System;
using System.Collections.Generic;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Avatar = Core.DTO.v1_0.Entities.Avatar;

namespace WebApp.Controllers
{
    public class AvatarController : BaseController
    {
        private readonly ICoreBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        
        
        public AvatarController(ICoreBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Avatar
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var thisUserAvatars = await _bll.Avatar.GetAllAsync(user!.Id);

            return View(thisUserAvatars);
        }

        // GET: Avatar/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _bll.Avatar
                .FirstOrDefaultAsync(id.Value);
            if (avatar == null)
            {
                return NotFound();
            }

            return View(avatar);
        }

        // GET: Avatar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avatar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sex,UploadedImage")] Core.BLL.DTO.Entities.Avatar avatar)
        {
            if (ModelState.IsValid)
            {
                if (avatar.UploadedImage != null && avatar.UploadedImage.Length > 0)
                {
                    
                    using (var memoryStream = new MemoryStream())
                    {
                        await avatar.UploadedImage.CopyToAsync(memoryStream);
                        avatar.Image = memoryStream.ToArray(); 
                    }
                }
                
                var user = await _userManager.GetUserAsync(User);

                await DisablePreviousAvatars(user!);
                
                avatar.Id = Guid.NewGuid();
                avatar.AppUserId = user!.Id;
                avatar.IsActive = true;
                avatar.Health = 100;
                avatar.Stamina = 100;
                avatar.Stress = 0;
                avatar.Hunger = 100;
                avatar.Strength = 10;
                avatar.Dexterity = 10;
                avatar.Intelligence = 10;
                avatar.Money = 100;
                avatar.Level = 1;
                avatar.Exp = 0;
                avatar.LastChanges = DateTime.UtcNow;
                // user.Avatars ??= new List<Avatar>();
                // user.Avatars.Add(avatar);
                

                // _uow.Users.Update(user);
                // await _uow.SaveChangesAsync();
                _bll.Avatar.Add(avatar);
                _bll.Logs.Add(new Logs()
                {
                    // AvatarId = avatar.Id,
                    Time = DateTime.UtcNow,
                    Message = "New AvatarCreated! " + avatar.ToString()
                });
                await _bll.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(avatar);
        }

        private async Task DisablePreviousAvatars(AppUser user)
        {
            var usersAvatars = await _bll.Avatar.GetByUserId(user.Id);
            if (usersAvatars.Any())
            {
                foreach (var a in usersAvatars)
                {
                    a.IsActive = false;

                    _bll.Avatar.Update(a);
                }
            }
            await _bll.SaveChangesAsync();
        }

        // GET: Avatar/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _bll.Avatar.FirstOrDefaultAsync(id.Value);
            if (avatar == null)
            {
                return NotFound();
            }
            return View(avatar);
        }
        

        // POST: Avatar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // "Health,Stamina,Hunger,Stress,Strength,Dexterity,Intelligence,Money,Sex,Level,Exp,Image,Id")] default edit binging
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UploadedImage,Id")] Core.BLL.DTO.Entities.Avatar avatar)
        {
            
            if (id != avatar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var oldAvatar = await _bll.Avatar.FirstOrDefaultAsync(id);
                    
                    if (oldAvatar!.AppUserId == user!.Id)
                    {
                        if (avatar.UploadedImage != null && avatar.UploadedImage.Length > 0)
                        {
                    
                            using (var memoryStream = new MemoryStream())
                            {
                                await avatar.UploadedImage.CopyToAsync(memoryStream);
                                oldAvatar.Image = memoryStream.ToArray(); 
                            }
                        }
                    }

                    _bll.Logs.Add(new Logs()
                    {
                        AvatarId = avatar.Id,
                        Time = DateTime.UtcNow,
                        Message = "Avatar updated! " + avatar.ToString()
                    });
                    _bll.Avatar.Update(oldAvatar);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvatarExists(avatar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(avatar);
        }

        // GET: Avatar/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _bll.Avatar
                .FirstOrDefaultAsync(id.Value);
            if (avatar == null)
            {
                return NotFound();
            }

            return View(avatar);
        }

        // POST: Avatar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var avatar = await _bll.Avatar.FirstOrDefaultAsync(id);
            if (avatar != null)
            {
                await _bll.Avatar.RemoveAsync(avatar);
            }

            _bll.Logs.Add(new Logs()
            {
                AvatarId = avatar.Id,
                Time = DateTime.UtcNow,
                Message = "Avatar deleted! " + avatar.ToString()
            });
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvatarExists(Guid id)
        {
            return _bll.Avatar.Exists(id);
        }
    }
}
