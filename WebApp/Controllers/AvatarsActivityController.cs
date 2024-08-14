using Core.BLL;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Domain.AddressTables;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using AvatarsActivity = Core.BLL.DTO.AddressTables.AvatarsActivity;

namespace WebApp.Controllers
{
    public class AvatarsActivityController : BaseController
    {
        private readonly ICoreBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public AvatarsActivityController(ICoreBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: AvatarsActivity
        public async Task<IActionResult> Index()
        {
            var res = await _bll.AvatarsActivity.GetWithDataAsync();
            List<AvatarsActivity> list = new List<AvatarsActivity>();
            foreach (var act in res)
            {
                if (!(await IsExpired(act.AvatarId)))
                {
                    list.Add(act);
                }
            }
            return View(list);
        }

        // GET: AvatarsActivity/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarsActivity = await _bll.AvatarsActivity
                .FirstOrDefaultAsync(id.Value);
            if (avatarsActivity == null)
            {
                return NotFound();
            }

            return View(avatarsActivity);
        }

        // GET: AvatarsActivity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvatarsActivity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeFinish,Start,Id")] Core.BLL.DTO.AddressTables.AvatarsActivity avatarsActivity)
        {
            if (ModelState.IsValid)
            {
                avatarsActivity.Id = Guid.NewGuid();
                _bll.AvatarsActivity.Add(avatarsActivity);
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "New avatars activity created! " + avatarsActivity.ToString()
                });
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avatarsActivity);
        }
        
        public async Task<IActionResult> CreateFromSelect(Guid activityId)
        {
            var user = await _userManager.GetUserAsync(User);
            var avatar = (await _bll.Avatar.GetByUserId(user.Id)).First(e => e.IsActive);
            bool check = await IsExpired(avatar.Id);
            if (!check) return RedirectToAction("Index", "Activity");
            
            var activity = _bll.Activity.GetById(activityId);
            var activityType = _bll.ActivityType.GetById(activity.ActivityTypeId);
            if (avatar!.Level < activity.LevelLimit) return RedirectToAction("Index", "Activity");
            
            if (avatar.Stress + activity.StressGain > 100) return RedirectToAction("Index", "Activity");
            
            DateTime finish = DateTime.Now.ToUniversalTime();
            finish = finish.AddDays(activityType.DaysToComplete);
            finish = finish.AddHours(activityType.HoursToComplete);
            finish = finish.AddMinutes(activityType.MinutesToComplete);
            
            var aa = new Core.BLL.DTO.AddressTables.AvatarsActivity()
            {
                ActivityId = activity.Id,
                AvatarId = avatar.Id,
                TimeFinish = finish,
                Start = DateTime.Now.ToUniversalTime()
            };

            _bll.Logs.Add(new Logs()
            {
                AvatarId = avatar.Id,
                Time = DateTime.Now.ToUniversalTime(),
                Message = "New avatars activity created! " + aa.ToString()
            });
            _bll.AvatarsActivity.Add(aa);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Index", "Activity");
        }

        private async Task<bool> IsExpired(Guid avatarId)
        {
            var avatarsActivity = _bll.AvatarsActivity.GetByAvatarId(avatarId);
            if (avatarsActivity == null) return true;
            
            if (DateTime.Now.ToUniversalTime() > avatarsActivity.TimeFinish)
            {
                var avatar = _bll.Avatar.GetById(avatarId);
                var act = _bll.Activity.GetById(avatarsActivity.ActivityId);
                var actType = _bll.ActivityType.GetById(act.ActivityTypeId);

                avatar!.Exp += actType.Exp;

                if (avatar.Exp > avatar.ExpToLevelUp)
                {
                    avatar.Exp -= avatar.ExpToLevelUp;
                    avatar.Level++;
                    avatar.ExpToLevelUp += avatar.ExpToLevelUp / 20;
                }
                
                avatar.Money += actType.Money;
                switch (act.Stat)
                {
                    case EStats.Dexterity:
                        avatar.Dexterity += actType.Value;
                        break;
                    case EStats.Intelligence:
                        avatar.Intelligence += actType.Value;
                        break;
                    case EStats.Strength:
                        avatar.Strength += actType.Value;
                        break;
                }

                avatar.Stress += act.StressGain;
                avatar.Stamina -= act.StaminaDrain;

                _bll.Logs.Add(new Logs()
                {
                    AvatarId = avatar.Id,
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Avatars activity is completed! " + avatarsActivity.ToString()
                });
                _bll.Avatar.Update(avatar);
                await _bll.AvatarsActivity.RemoveAsync(avatarsActivity);
                await _bll.SaveChangesAsync();
                return true;
            }

            return false;
        }
        
        // GET: AvatarsActivity/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarsActivity = await _bll.AvatarsActivity.FirstOrDefaultAsync(id.Value);
            if (avatarsActivity == null)
            {
                return NotFound();
            }
            return View(avatarsActivity);
        }

        // POST: AvatarsActivity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TimeFinish,Start,Id")] AvatarsActivity avatarsActivity)
        {
            if (id != avatarsActivity.Id)
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
                        Message = "Avatars activity updated! " + avatarsActivity.ToString()
                    });
                    _bll.AvatarsActivity.Update(avatarsActivity);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvatarsActivityExists(avatarsActivity.Id))
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
            return View(avatarsActivity);
        }

        // GET: AvatarsActivity/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatarsActivity = await _bll.AvatarsActivity
                .FirstOrDefaultAsync(id.Value);
            if (avatarsActivity == null)
            {
                return NotFound();
            }

            return View(avatarsActivity);
        }

        // POST: AvatarsActivity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var avatarsActivity = await _bll.AvatarsActivity.FirstOrDefaultAsync(id);
            if (avatarsActivity != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Avatars activity deleted! " + avatarsActivity.ToString()
                });
                await _bll.AvatarsActivity.RemoveAsync(avatarsActivity);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvatarsActivityExists(Guid id)
        {
            return _bll.AvatarsActivity.Exists(id);
        }
    }
}
