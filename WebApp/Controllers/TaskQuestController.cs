using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    // [Area("Admin")]
    public class TaskQuestController : Controller
    {
        private readonly ICoreBLL _bll;

        public TaskQuestController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/TaskQuest
        public async Task<IActionResult> Index()
        {
            return View(await _bll.TaskQuest.GetWithDataAsync());
        }

        // GET: Admin/TaskQuest/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskQuest = await _bll.TaskQuest
                .FirstOrDefaultAsync(id.Value);
            if (taskQuest == null)
            {
                return NotFound();
            }

            return View(taskQuest);
        }

        // GET: Admin/TaskQuest/Create
        public async Task<IActionResult> Create()
        {
            var vm = new TaskQuestCreateEditeModel()
            {
                Types = new SelectList(await _bll.TaskType.GetAllAsync(), "Id", "Difficulty"),
                Activities = new SelectList(await _bll.Activity.GetAllAsync(), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Admin/TaskQuest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskQuestCreateEditeModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "New TaskQuest created! " + vm.TaskQuest.ToString()
                });
                _bll.TaskQuest.Add(vm.TaskQuest);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Types = new SelectList(await _bll.TaskType.GetAllAsync(), "Id", "Difficulty");
            vm.Activities = new SelectList(await _bll.Activity.GetAllAsync(), "Id", "Name");
            return View(vm);
        }

        // GET: Admin/TaskQuest/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskQuest = await _bll.TaskQuest.FirstOrDefaultAsync(id.Value);
            if (taskQuest == null)
            {
                return NotFound();
            }
            var vm = new TaskQuestCreateEditeModel()
            {
                Types = new SelectList(await _bll.TaskType.GetAllAsync(), "Id", "Difficulty"),
                Activities = new SelectList(await _bll.Activity.GetAllAsync(), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Admin/TaskQuest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TaskQuestCreateEditeModel vm)
        {
            if (id != vm.TaskQuest.Id)
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
                        Message = "TaskQuest updated! " + vm.TaskQuest.ToString()
                    });
                    _bll.TaskQuest.Update(vm.TaskQuest);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskQuestExists(vm.TaskQuest.Id))
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

            vm.Types = new SelectList(await _bll.TaskType.GetAllAsync(), "Id", "Difficulty");
            vm.Activities = new SelectList(await _bll.Activity.GetAllAsync(), "Id", "Name");
            return View(vm);
        }

        // GET: Admin/TaskQuest/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskQuest = await _bll.TaskQuest
                .FirstOrDefaultAsync(id.Value);
            if (taskQuest == null)
            {
                return NotFound();
            }

            return View(taskQuest);
        }

        // POST: Admin/TaskQuest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var taskQuest = await _bll.TaskQuest.FirstOrDefaultAsync(id);
            if (taskQuest != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "TaskQuest deleted! " + taskQuest.ToString()
                });
                await _bll.TaskQuest.RemoveAsync(taskQuest);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskQuestExists(Guid id)
        {
            return _bll.TaskQuest.Exists(id);
        }
    }
}
