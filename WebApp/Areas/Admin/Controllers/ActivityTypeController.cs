using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;
using ActivityType = Core.BLL.DTO.Entities.ActivityType;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActivityTypeController : Controller
    {
        private readonly ICoreBLL _bll;

        public ActivityTypeController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/ActivityType
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ActivityType.GetAllAsync());
        }

        // GET: Admin/ActivityType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await _bll.ActivityType
                .FirstOrDefaultAsync(id.Value);
            if (activityType == null)
            {
                return NotFound();
            }

            return View(activityType);
        }

        // GET: Admin/ActivityType/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ActivityTypeCreateEditeViewModel();
            return View(vm);
        }

        // POST: Admin/ActivityType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityTypeCreateEditeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "New activity type created! " + vm.ActivityType.ToString()
                });
                _bll.ActivityType.Add(vm.ActivityType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Admin/ActivityType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await _bll.ActivityType.FirstOrDefaultAsync(id.Value);
            if (activityType == null)
            {
                return NotFound();
            }
            return View(new ActivityTypeCreateEditeViewModel() {ActivityType = activityType});
        }

        // POST: Admin/ActivityType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,DaysToComplete,HoursToComplete,MinutesToComplete,Exp,Value,Id")] ActivityType activityType)
        {
            if (id != activityType.Id)
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
                        Message = "Activity type updated! " + activityType.ToString()
                    });
                    _bll.ActivityType.Update(activityType);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityTypeExists(activityType.Id))
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
            return View(new ActivityTypeCreateEditeViewModel() {ActivityType = activityType});
        }

        // GET: Admin/ActivityType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await _bll.ActivityType
                .FirstOrDefaultAsync(id.Value);
            if (activityType == null)
            {
                return NotFound();
            }

            return View(activityType);
        }

        // POST: Admin/ActivityType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activityType = await _bll.ActivityType.FirstOrDefaultAsync(id);
            if (activityType != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "Activity type deleted! " + activityType.ToString()
                });
                await _bll.ActivityType.RemoveAsync(activityType);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityTypeExists(Guid id)
        {
            return _bll.ActivityType.Exists(id);
        }
    }
}
