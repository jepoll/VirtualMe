using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Domain.AddressTables;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.ViewModels;
using WebApp.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    public class RewardController : Controller
    {
        private readonly ICoreBLL _bll;

        public RewardController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/Reward
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Reward.GetWithDataAsync());
        }

        // GET: Admin/Reward/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _bll.Reward
                .FirstOrDefaultAsync(id.Value);
            if (reward == null)
            {
                return NotFound();
            }

            return View(reward);
        }

        // GET: Admin/Reward/Create
        public async Task<IActionResult> Create()
        {
            var vm = new RewardCreateEditeViewModel()
            {
                TaskQuests = new SelectList(await _bll.TaskQuest.GetWithDataAsync(), "Id", "Activity.Name"),
                Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Admin/Reward/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RewardCreateEditeViewModel vm)
        {
            var limits = _bll.TaskQuest.GetByIdWithData(vm.Reward.TaskQuestId).TaskType!.RewardPriceLimit;
            var item = _bll.Item.GetItemByIdValue(vm.Reward.ItemId.ToString());

            vm.Reward.Quantity = (int) (limits / item.Price);
            
            if (ModelState.IsValid)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "New reward created! " + vm.Reward.ToString()
                });
                _bll.Reward.Add(vm.Reward);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.TaskQuests = new SelectList(await _bll.TaskQuest.GetWithDataAsync(), "Id", "Activity.Name");
            vm.Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name");
            return View(vm);
        }

        // GET: Admin/Reward/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _bll.Reward.FirstOrDefaultAsync(id.Value);
            if (reward == null)
            {
                return NotFound();
            }
            var vm = new RewardCreateEditeViewModel()
            {
                TaskQuests = new SelectList(await _bll.TaskQuest.GetWithDataAsync(), "Id", "Activity.Name"),
                Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Admin/Reward/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RewardCreateEditeViewModel vm)
        {
            if (id != vm.Reward.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Logs.Add(new Logs()
                    {
                        Time = DateTime.Now.ToUniversalTime(),
                        Message = "Reward updated! " + vm.Reward.ToString()
                    });
                    _bll.Reward.Update(vm.Reward);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RewardExists(vm.Reward.Id))
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

            vm.TaskQuests = new SelectList(await _bll.TaskQuest.GetWithDataAsync(), "Id", "Activity.Name");
            vm.Items = new SelectList(await _bll.Item.GetAllAsync(), "Id", "Name");
            return View(vm);;
        }

        // GET: Admin/Reward/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _bll.Reward
                .FirstOrDefaultAsync(id.Value);
            if (reward == null)
            {
                return NotFound();
            }

            return View(reward);
        }

        // POST: Admin/Reward/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reward = await _bll.Reward.FirstOrDefaultAsync(id);
            if (reward != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Reward deleted! " + reward.ToString()
                });
                await _bll.Reward.RemoveAsync(reward);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RewardExists(Guid id)
        {
            return _bll.Reward.Exists(id);
        }
    }
}
