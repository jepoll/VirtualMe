using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.BLL.DTO.AddressTables;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using AvatarsTasks = Core.BLL.DTO.AddressTables.AvatarsTasks;

namespace WebApp.Controllers
{
    public class AvatarsTasksController : BaseController
    {
        private readonly ICoreBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly Random _rnd = new Random();

        public AvatarsTasksController(ICoreBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: AvatarsTasks
        public async Task<IActionResult> Index()
        {
            var avatar = (await _bll.Avatar.GetByUserId(((await _userManager.GetUserAsync(User))!).Id))
                .First(e => e.IsActive);
            
            var toGen = await RemoveExpiredOrCompleted();
            var tasks = await _bll.AvatarsTasks.GetByAvatarIdWithDataAsync(avatar.Id);
            if (!tasks.Any()) //Empty - generate 3 tasks
            {
                var taskQuests = (await _bll.TaskQuest.GetWithDataAsync()).ToList();
                if (taskQuests == null || taskQuests.Count == 0) return View(await _bll.AvatarsTasks.GetByAvatarIdWithDataAsync(avatar.Id));
                for (int num = 0; num < 3; num++)
                {
                    var taskQuest = taskQuests[_rnd.Next(0, taskQuests.Count)];
                    var aa = new AvatarsTasks()
                    {
                        TaskQuestId = taskQuest.Id,
                        AvatarId = avatar.Id,
                        TimeStart = DateTime.Now.ToUniversalTime(),
                        CurrentState = 0
                    };
                    _bll.Logs.Add(new Logs()
                    {
                        AvatarId = avatar.Id,
                        Time = DateTime.Now.ToUniversalTime(),
                        Message = "Avatar got new task! " + aa.ToString()
                    });
                    _bll.AvatarsTasks.Add(aa);
                }
                await _bll.SaveChangesAsync();
            }
            return View(await _bll.AvatarsTasks.GetByAvatarIdWithDataAsync(avatar.Id));
        }

        private async Task<int> RemoveExpiredOrCompleted()
        {
            int deletionCounter = 0;
            var avatar = (await _bll.Avatar.GetByUserId(((await _userManager.GetUserAsync(User))!).Id))
                .First(e => e.IsActive);
            
            var tasks = await _bll.AvatarsTasks.GetByAvatarIdWithDataAsync(avatar.Id);
            foreach (var task in tasks)
            {
                if (task.CurrentState >= task.TaskQuest.TaskType!.Goal) // case Completed
                {
                    var data = task.TaskQuest.TaskType;

                    avatar.Exp += data.Exp;
                    avatar.Money += data.Money;

                    var reward = _bll.Reward.GetByTaskQuestId(task.TaskQuest.Id);
                    var owns = new Owns()
                    {
                        AvatarId = avatar.Id,
                        ItemId = reward.ItemId,
                        Amount = reward.Quantity,
                    };

                    _bll.Owns.Add(owns);
                    await _bll.AvatarsTasks.RemoveAsync(task);
                    deletionCounter++;
                    continue;
                }

                var timeFinish = task.TimeStart;
                timeFinish = timeFinish.AddDays(task.TaskQuest.TaskType.DaysToComplete);
                timeFinish = timeFinish.AddHours(task.TaskQuest.TaskType.HoursToComplete);
                timeFinish = timeFinish.AddMinutes(task.TaskQuest.TaskType.MinutesToComplete);
                if (timeFinish < DateTime.Now.ToUniversalTime()) // case just expired
                {
                    _bll.Logs.Add(new Logs()
                    {
                        AvatarId = avatar.Id,
                        Time = DateTime.Now.ToUniversalTime(),
                        Message = "Some task were expired! " + task.ToString()
                    });
                    await _bll.AvatarsTasks.RemoveAsync(task);
                    deletionCounter++;
                }
            }

            _bll.Avatar.Update(avatar);
            await _bll.SaveChangesAsync();
            return deletionCounter;
        }

        // GET: AvatarsTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarsTasks = await _bll.AvatarsTasks
                .FirstOrDefaultAsync(id.Value);
            if (avatarsTasks == null)
            {
                return NotFound();
            }

            return View(avatarsTasks);
        }

        // GET: AvatarsTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvatarsTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeStart,TimeFinish,Id")] Core.BLL.DTO.AddressTables.AvatarsTasks avatarsTasks)
        {
            if (ModelState.IsValid)
            {
                avatarsTasks.Id = Guid.NewGuid();
                _bll.AvatarsTasks.Add(avatarsTasks);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avatarsTasks);
        }

        // GET: AvatarsTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarsTasks = await _bll.AvatarsTasks.FirstOrDefaultAsync(id.Value);
            if (avatarsTasks == null)
            {
                return NotFound();
            }
            return View(avatarsTasks);
        }

        // POST: AvatarsTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TimeStart,TimeFinish,Id")] AvatarsTasks avatarsTasks)
        {
            if (id != avatarsTasks.Id)
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
                        Message = "Task was changed! " + avatarsTasks.ToString()
                    });
                    _bll.AvatarsTasks.Update(avatarsTasks);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvatarsTasksExists(avatarsTasks.Id))
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
            return View(avatarsTasks);
        }

        // GET: AvatarsTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarsTasks = await _bll.AvatarsTasks
                .FirstOrDefaultAsync(id.Value);
            if (avatarsTasks == null)
            {
                return NotFound();
            }

            return View(avatarsTasks);
        }

        // POST: AvatarsTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var avatarsTasks = await _bll.AvatarsTasks.FirstOrDefaultAsync(id);
            if (avatarsTasks != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Task was Deleted! " + avatarsTasks.ToString()
                });
                await _bll.AvatarsTasks.RemoveAsync(avatarsTasks);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvatarsTasksExists(Guid id)
        {
            return _bll.AvatarsTasks.Exists(id);
        }
    }
}
