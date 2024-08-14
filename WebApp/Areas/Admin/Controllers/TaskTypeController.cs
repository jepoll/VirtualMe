using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaskTypeController : Controller
    {
        private readonly ICoreBLL _bll;

        public TaskTypeController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/TaskType
        public async Task<IActionResult> Index()
        {
            return View(await _bll.TaskType.GetAllAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowTaskTypes()
        {
            return View(await _bll.TaskType.GetAllAsync());
        }

        // GET: Admin/TaskType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _bll.TaskType
                .FirstOrDefaultAsync(id.Value);
            if (taskType == null)
            {
                return NotFound();
            }

            return View(taskType);
        }

        // GET: Admin/TaskType/Create
        public IActionResult Create()
        {
            var vm = new TaskTypeCreateEditeViewModel()
            {
                Difficulties = new SelectList(Enum.GetValues(typeof(EDifficulty)))
            };
            return View(vm);
        }

        // POST: Admin/TaskType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskTypeCreateEditeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "New task type created! " + vm.TaskType.ToString()
                });
                _bll.TaskType.Add(vm.TaskType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Difficulties = new SelectList(Enum.GetValues(typeof(EDifficulty)));
            return View(vm);
        }

        // GET: Admin/TaskType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _bll.TaskType.FirstOrDefaultAsync(id.Value);
            if (taskType == null)
            {
                return NotFound();
            }
            var vm = new TaskTypeCreateEditeViewModel()
            {
                Difficulties = new SelectList(Enum.GetValues(typeof(EDifficulty)))
            };
            return View(vm);
        }

        // POST: Admin/TaskType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TaskTypeCreateEditeViewModel vm)
        {
            if (id != vm.TaskType.Id)
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
                        Message = "Task type updated! " + vm.TaskType.ToString()
                    });
                    _bll.TaskType.Update(vm.TaskType);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskTypeExists(vm.TaskType.Id))
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
            vm.Difficulties = new SelectList(Enum.GetValues(typeof(EDifficulty)));
            return View(vm);
        }

        // GET: Admin/TaskType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskType = await _bll.TaskType
                .FirstOrDefaultAsync(id.Value);
            if (taskType == null)
            {
                return NotFound();
            }

            return View(taskType);
        }

        // POST: Admin/TaskType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var taskType = await _bll.TaskType.FirstOrDefaultAsync(id);
            if (taskType != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now,
                    Message = "Task type deleted! " + taskType.ToString()
                });
                await _bll.TaskType.RemoveAsync(taskType);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskTypeExists(Guid id)
        {
            return _bll.TaskType.Exists(id);
        }
    }
}
