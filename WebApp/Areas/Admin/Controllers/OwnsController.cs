using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OwnsController : Controller
    {
        private readonly ICoreBLL _bll;

        public OwnsController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/Owns
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Owns.GetAllWIthDataAsync());
        }

        // GET: Admin/Owns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owns = await _bll.Owns
                .FirstOrDefaultAsync(id.Value);
            if (owns == null)
            {
                return NotFound();
            }

            return View(owns);
        }

        // GET: Admin/Owns/Create
        public async Task<IActionResult> Create()
        {
            var vm = new OwnsCreateEditeViewModel()
            {
                Avatars = new SelectList(await _bll.Avatar.GetAllAsync(), "Id", "Id"),
                Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Admin/Owns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnsCreateEditeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "New owns created! " + vm.Owns.ToString()
                });
                _bll.Owns.Add(vm.Owns);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Avatars = new SelectList(await _bll.Avatar.GetAllAsync(), "Id", "Id");
            vm.Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name");
            return View(vm);
        }

        // GET: Admin/Owns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owns = await _bll.Owns.FirstOrDefaultAsync(id.Value);
            if (owns == null)
            {
                return NotFound();
            }
            var vm = new OwnsCreateEditeViewModel()
            {
                Avatars = new SelectList(await _bll.Avatar.GetAllAsync(), "Id", "Id"),
                Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Admin/Owns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OwnsCreateEditeViewModel vm)
        {
            if (id != vm.Owns.Id)
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
                        Message = "Owns updated! " + vm.Owns.ToString()
                    });
                    _bll.Owns.Update(vm.Owns);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnsExists(vm.Owns.Id))
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
            vm.Avatars = new SelectList(await _bll.Avatar.GetAllAsync(), "Id", "Id");
            vm.Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name");
            return View(vm);
        }

        // GET: Admin/Owns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owns = await _bll.Owns
                .FirstOrDefaultAsync(id.Value);
            if (owns == null)
            {
                return NotFound();
            }

            return View(owns);
        }

        // POST: Admin/Owns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var owns = await _bll.Owns.FirstOrDefaultAsync(id);
            if (owns != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "Owns deleted! " + owns.ToString()
                });
                await _bll.Owns.RemoveAsync(owns);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnsExists(Guid id)
        {
            return _bll.Owns.Exists(id);
        }
    }
}
