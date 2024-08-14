using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.BLL.DTO.AddressTables;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.DAL.EF;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ItemController : BaseController
    {
        private readonly ICoreBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public ItemController(ICoreBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SellItem(string id)
        {
            var avatar = (await _bll.Avatar.GetByUserId(
                    (await _userManager.GetUserAsync(User))!.Id)
                ).FirstOrDefault(e => e.IsActive);
            var owns = _bll.Owns.GetByIdString(id);
            if (avatar == null || owns == null) return RedirectToAction("Index", "Home");

            var item = await _bll.Item.FirstOrDefaultAsync(owns.ItemId);
            avatar.Money += item!.Price / 2;
            _bll.Avatar.Update(avatar);

            if (owns.Amount > 1)
            {
                owns.Amount -= 1;
                _bll.Owns.Update(owns);
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Owns updated! " + owns.ToString()
                });
            }
            else
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Owns deleted! " + owns.ToString()
                });
                await _bll.Owns.RemoveAsync(owns);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UseItem(Guid id)
        {
            var owns = await _bll.Owns.GetByIdWithData(id);
            var avatar = owns.Avatar;
            if (owns.Item!.IsConsumable)
            {
                if (owns.Amount > 1)
                {
                    owns.Amount--;
                    if (avatar!.Hunger + 10 > 100) avatar.Hunger = 100;
                    else avatar.Hunger += 10;
                    _bll.Owns.Update(owns);
                    _bll.Avatar.Update(avatar);
                }
                else
                {
                    if (avatar!.Hunger + 10 > 100) avatar.Hunger = 100;
                    else avatar.Hunger += 10;
                    _bll.Avatar.Update(avatar);
                    await _bll.Owns.RemoveAsync(owns);
                }
            }
            else
            {
                owns.IsEquipped = !owns.IsEquipped;
                _bll.Owns.Update(owns);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Item.GetAllAsync());
        }

        public async Task<IActionResult> BuyItem(Guid itemId)
        {
            var user = await _userManager.GetUserAsync(User);
            var avatar = (await _bll.Avatar.GetByUserId(user.Id)).First(e => e.IsActive);
            var items = await _bll.Item.GetAllAsync();
            var item = items.First(i => i.Id.Equals(itemId));

            if (avatar.Money >= item.Price)
            {
                var avatarInventory = await _bll.Owns.GetAllByAvatarIdAsync(avatar.Id);
                avatar.Money -= item.Price;

                var owns = avatarInventory.FirstOrDefault(e => e.AvatarId.Equals(avatar.Id) && e.ItemId.Equals(item.Id));

                if (owns != null)
                {
                    owns.Amount += 1;
                    _bll.Owns.Update(owns);
                    _bll.Logs.Add(new Logs()
                    {
                        Time = DateTime.Now.ToUniversalTime(),
                        Message = "Owns updated! " + owns.ToString()
                    });
                }
                else
                {
                    owns = new Owns()
                    {
                        AvatarId = avatar.Id,
                        ItemId = item.Id,
                        Amount = 1,
                        IsEquipped = false
                    };
                    _bll.Logs.Add(new Logs()
                    {
                        Time = DateTime.Now.ToUniversalTime(),
                        Message = "Owns created! " + owns.ToString()
                    });
                    _bll.Owns.Add(owns);
                }

                _bll.Avatar.Update(avatar);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction("Index", "Home");
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Item
                .FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            var vm = new ItemCreateEditeViewModel()
            {
                Rarities = new SelectList(Enum.GetValues(typeof(ERarity))),
                Slots = new SelectList(Enum.GetValues(typeof(ESlot))),
                Stats = new SelectList(Enum.GetValues(typeof(EStats)))
            };
            return View(vm);
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCreateEditeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Item.IsConsumable)
                {
                    vm.Item.StatToUpgrade = null;
                    vm.Item.Slot = null;
                    vm.Item.ObjectModel = null;
                }

                if (vm.Item.UploadedImage != null && vm.Item.UploadedImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await vm.Item.UploadedImage!.CopyToAsync(memoryStream);
                        vm.Item.Image = memoryStream.ToArray(); 
                    }
                }

                if (vm.Item.ObjectModel != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await vm.Item.ObjectModel!.CopyToAsync(memoryStream);
                        vm.Item.Object = memoryStream.ToArray();
                    }
                }
                
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Item created! " + vm.Item.ToString()
                });
                _bll.Item.Add(vm.Item);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.Rarities = new SelectList(Enum.GetValues(typeof(ERarity)));
            vm.Slots = new SelectList(Enum.GetValues(typeof(ESlot)));
            vm.Stats = new SelectList(Enum.GetValues(typeof(EStats)));
            return View(vm);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Item.FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            var vm = new ItemCreateEditeViewModel()
            {
                Rarities = new SelectList(Enum.GetValues(typeof(ERarity))),
                Slots = new SelectList(Enum.GetValues(typeof(ESlot))),
                Stats = new SelectList(Enum.GetValues(typeof(EStats)))
            };
            return View(vm);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemCreateEditeViewModel vm)
        {
            if (id != vm.Item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Item.Update(vm.Item);
                    _bll.Logs.Add(new Logs()
                    {
                        Time = DateTime.Now.ToUniversalTime(),
                        Message = "Item updated! " + vm.Item.ToString()
                    });
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(vm.Item.Id))
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
            vm.Rarities = new SelectList(Enum.GetValues(typeof(ERarity)));
            vm.Slots = new SelectList(Enum.GetValues(typeof(ESlot)));
            vm.Stats = new SelectList(Enum.GetValues(typeof(EStats)));
            return View(vm);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Item
                .FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _bll.Item.FirstOrDefaultAsync(id);
            if (item != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Item deleted! " + item.ToString()
                });
                await _bll.Item.RemoveAsync(item);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return _bll.Item.Exists(id);
        }
    }
}
