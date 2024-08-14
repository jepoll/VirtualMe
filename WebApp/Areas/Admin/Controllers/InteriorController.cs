using Core.BLL.DTO.AddressTables;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.ViewModels;
using WebApp.Controllers;
using Interior = Core.BLL.DTO.Entities.Interior;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InteriorController : BaseController
    {
        private readonly ICoreBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public InteriorController(ICoreBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }
        
        // GET: Admin/Interior
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Interior.GetAllAsync());
        }

        public async Task<IActionResult> InteriorShop()
        {
            var interiors = await _bll.Interior.GetAllAsync();
            var avatar = (await _bll.Avatar.GetByUserId((await _userManager.GetUserAsync(User))!.Id)).First(e => e.IsActive);
            var avatarOwnsInterior = await _bll.AvatarOwnsInterior.GetAllByAvatarsIdAsync(avatar.Id);
            var avatarsInteriorsIds = avatarOwnsInterior.Select(e => e.InteriorId);
            
            //3 Level Max
            //var firstLevel = interiors.Where(e => e.Level == 1).ToList();
            var toDisplay = new List<Interior>();
            foreach (var item in interiors)
            {
                // if (avatarsInteriorsIds.Contains(item.Id))
                // {
                //     firstLevel.Remove(item);
                //     if (item.Level == 1)
                //     {
                //         var newInterior = interiors.First(e => e.Name.Equals(item.Name) && e.Level == 2);
                //         if (firstLevel.Contains(newInterior))
                //         {
                //             firstLevel.Add(interiors.First(e => e.Name.Equals(item.Name) && e.Level == 3));
                //         }
                //         
                //     } else if (item.Level == 2)
                //     {
                //         firstLevel.Add(interiors.First(e => e.Name.Equals(item.Name) && e.Level == 3));
                //     }
                // }
                // if (firstLevel.FirstOrDefault(e => e.Name.Equals(item.Name) && e.Level == item.Level + 1) != null)
                // {
                //     firstLevel.Remove(item);
                // }

                if (avatarsInteriorsIds.Contains(item.Id))
                {
                    continue;
                }
                else
                {
                    if(toDisplay.FirstOrDefault(e => e.Name.Equals(item.Name) && e.Level == item.Level - 1) != null) continue;
                    toDisplay.Add(item);
                }
            }
            return View(toDisplay);
        }

        public async Task<IActionResult> BuyInterior(Guid id)
        {
            var interior = _bll.Interior.GetById(id);
            var avatar = (await _bll.Avatar.GetByUserId((await _userManager.GetUserAsync(User))!.Id)).First(e => e.IsActive);
            if (avatar.Money < interior.Cost) return RedirectToAction("InteriorShop");
            avatar.Money -= interior.Cost;
            var ownsInterior = new AvatarOwnsInterior()
            {
                AvatarId = avatar.Id,
                InteriorId = interior.Id,
                PurchaseDate = DateTime.Now
            };
            _bll.Logs.Add(new Logs()
            {
                AvatarId = avatar.Id,
                Time = DateTime.Now,
                Message = "Avatar bought Interior! " + ownsInterior.ToString()
            });
            _bll.AvatarOwnsInterior.Add(ownsInterior);
            _bll.Avatar.Update(avatar);
            await _bll.SaveChangesAsync();
            return RedirectToAction("InteriorShop");
        }

        // GET: Admin/Interior/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interior = await _bll.Interior
                .FirstOrDefaultAsync(id.Value);
            if (interior == null)
            {
                return NotFound();
            }

            return View(interior);
        }

        // GET: Admin/Interior/Create
        public IActionResult Create()
        {
            var vm = new InteriorCreateEditeViewModel()
            {
                StatList = new SelectList(Enum.GetValues(typeof(EStats))),
            };
            return View(vm);
        }

        // POST: Admin/Interior/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InteriorCreateEditeViewModel vm)
        {
            //System.Console.Out.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "New interior created! " + vm.Interior.ToString()
                });
                _bll.Interior.Add(vm.Interior);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.StatList = new SelectList(Enum.GetValues(typeof(EStats)));
            return View(vm);
        }

        // GET: Admin/Interior/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interior = await _bll.Interior.FirstOrDefaultAsync(id.Value);
            if (interior == null)
            {
                return NotFound();
            }
            var vm = new InteriorCreateEditeViewModel()
            {
                StatList = new SelectList(Enum.GetValues(typeof(EStats))),
            };  
            return View(vm);
        }

        // POST: Admin/Interior/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InteriorCreateEditeViewModel vm)
        {
            if (id != vm.Interior.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Logs.Add(new Logs()
                    {
                        Time = DateTime.Now,
                        Message = "Interior updated! " + vm.Interior.ToString()
                    });
                    _bll.Interior.Update(vm.Interior);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteriorExists(vm.Interior.Id))
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
            vm.StatList = new SelectList(Enum.GetValues(typeof(EStats)));
            return View(vm);
        }

        // GET: Admin/Interior/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interior = await _bll.Interior
                .FirstOrDefaultAsync(id.Value);
            if (interior == null)
            {
                return NotFound();
            }

            return View(interior);
        }

        // POST: Admin/Interior/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var interior = await _bll.Interior.FirstOrDefaultAsync(id);
            if (interior != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "Interior deleted! " + interior.ToString()
                });
                await _bll.Interior.RemoveAsync(interior);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InteriorExists(Guid id)
        {
            return _bll.Interior.Exists(id);
        }
    }
}
