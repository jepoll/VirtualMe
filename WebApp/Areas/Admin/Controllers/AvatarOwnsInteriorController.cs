using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AvatarOwnsInteriorController : Controller
    {
        private readonly ICoreBLL _bll;

        public AvatarOwnsInteriorController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/AvatarOwnsInterior
        public async Task<IActionResult> Index()
        {
            return View(await _bll.AvatarOwnsInterior.GetAllAsync());
        }

        // GET: Admin/AvatarOwnsInterior/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarOwnsInterior = await _bll.AvatarOwnsInterior
                .FirstOrDefaultAsync(id.Value);
            if (avatarOwnsInterior == null)
            {
                return NotFound();
            }

            return View(avatarOwnsInterior);
        }

        // GET: Admin/AvatarOwnsInterior/Create
        public async Task<IActionResult> Create()
        {
            var vm = new AvatarOwnsInteriorCreateEditeViewModel()
            {
                AvatarId = new SelectList(await _bll.Avatar.GetAllAsync(), nameof(Avatar.Id)),
                InteriorId = new SelectList(await _bll.Interior.GetAllAsync(), nameof(Interior.Id))
            };
            return View(vm);
        }

        // POST: Admin/AvatarOwnsInterior/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AvatarOwnsInteriorCreateEditeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "New AvatarOwnInterior created! " +vm.OwnsInterior.ToString()
                });
                _bll.AvatarOwnsInterior.Add(vm.OwnsInterior);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.AvatarId = new SelectList(await _bll.Avatar.GetAllAsync(), nameof(Avatar.Id));
            vm.InteriorId = new SelectList(await _bll.Interior.GetAllAsync(), nameof(Interior.Id));
            return View(vm);
        }

        // GET: Admin/AvatarOwnsInterior/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarOwnsInterior = await _bll.AvatarOwnsInterior.FirstOrDefaultAsync(id.Value);
            if (avatarOwnsInterior == null)
            {
                return NotFound();
            }
            var vm = new AvatarOwnsInteriorCreateEditeViewModel()
            {
                AvatarId = new SelectList(await _bll.Avatar.GetAllAsync(), nameof(Avatar.Id)),
                InteriorId = new SelectList(await _bll.Interior.GetAllAsync(), nameof(Interior.Id))
            };
            return View(vm);
        }

        // POST: Admin/AvatarOwnsInterior/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AvatarOwnsInteriorCreateEditeViewModel vm)
        {
            if (id != vm.OwnsInterior.Id)
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
                        Message = "AvatarOwnsInterior updated! " + vm.OwnsInterior.ToString()
                    });
                    _bll.AvatarOwnsInterior.Update(vm.OwnsInterior);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvatarOwnsInteriorExists(vm.OwnsInterior.Id))
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
            vm.AvatarId = new SelectList(await _bll.Avatar.GetAllAsync(), nameof(Avatar.Id));
            vm.InteriorId = new SelectList(await _bll.Interior.GetAllAsync(), nameof(Interior.Id));
            return View(vm);
        }

        // GET: Admin/AvatarOwnsInterior/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarOwnsInterior = await _bll.AvatarOwnsInterior
                .FirstOrDefaultAsync(id.Value);
            if (avatarOwnsInterior == null)
            {
                return NotFound();
            }

            return View(avatarOwnsInterior);
        }

        // POST: Admin/AvatarOwnsInterior/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var avatarOwnsInterior = await _bll.AvatarOwnsInterior.FirstOrDefaultAsync(id);
            if (avatarOwnsInterior != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "AvatarOwnsInterior deleted! " + avatarOwnsInterior.ToString()
                });
                await _bll.AvatarOwnsInterior.RemoveAsync(avatarOwnsInterior);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvatarOwnsInteriorExists(Guid id)
        {
            return _bll.AvatarOwnsInterior.Exists(id);
        }
    }
}
