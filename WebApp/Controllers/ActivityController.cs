using Core.BLL.DTO.AddressTables;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Enums;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using WebApp.ViewModels;
using Avatar = Core.DAL.DTO.Entities.Avatar;

namespace WebApp.Controllers
{
    public class ActivityController : Controller
    {
        private readonly ICoreBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public ActivityController(ICoreBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Activity
        public async Task<IActionResult> Index()
        {
            var res = await _bll.Activity.GetAllAsync();
            res = res.Select(e =>
            {
                e.ActivityType = _bll.ActivityType.GetById(e.ActivityTypeId);
                return e;
            });

            var avatars = await _bll.Avatar.GetByUserId(((await _userManager.GetUserAsync(User)!)!).Id);
            var avatar = avatars.First(a => a.IsActive);
            var current = _bll.AvatarsActivity.GetByAvatarId(avatar.Id);
            if (!(await IsExpired(current, avatar)))
            {
                current.Activity = res.FirstOrDefault(e => e.Id.Equals(current.ActivityId));
            }
            else current = null;
            var vm = new ActivityIndexViewModel()
            {
                Activities = res,
                Activity = current
            };
            return View(vm);
        }

        private async Task<bool> IsExpired(AvatarsActivity aa, Core.BLL.DTO.Entities.Avatar avatar)
        {
            if (aa == null) return true;

            if (DateTime.Now.ToUniversalTime() > aa.TimeFinish)
            {
                var act = _bll.Activity.GetById(aa.ActivityId);
                var actType = _bll.ActivityType.GetById(act.ActivityTypeId);

                avatar!.Exp += actType.Exp;

                if (avatar.Exp > avatar.ExpToLevelUp)
                {
                    avatar.Exp -= avatar.ExpToLevelUp;
                    avatar.Level++;
                    avatar.ExpToLevelUp += avatar.ExpToLevelUp / 20;
                }

                avatar.Money += actType.Money;

                var interiors = (await _bll.AvatarOwnsInterior.GetAllByAvatarIdWithInteriorAsync(avatar.Id))
                    .Select(e => e.Interior);
                float modifier = 1;
                switch (act.Stat)
                {
                    case EStats.Dexterity:
                        if (interiors.IsNullOrEmpty())
                        {
                            avatar.Dexterity += actType.Value;
                            break;
                        }
                        foreach (var item in interiors)
                        {
                            if (item.Stat.Equals(EStats.Dexterity)) modifier += item.Boost;
                        }
                        avatar.Dexterity += (int) Math.Round(modifier * actType.Value, MidpointRounding.AwayFromZero);
                        break;
                    case EStats.Intelligence:
                        if (interiors.IsNullOrEmpty())
                        {
                            avatar.Dexterity += actType.Value;
                            break;
                        }
                        foreach (var item in interiors)
                        {
                            if (item.Stat.Equals(EStats.Intelligence)) modifier += item.Boost;
                        }
                        avatar.Intelligence += (int) Math.Round(modifier * actType.Value, MidpointRounding.AwayFromZero);
                        break;
                    case EStats.Strength:
                        if (interiors.IsNullOrEmpty())
                        {
                            avatar.Dexterity += actType.Value;
                            break;
                        }
                        foreach (var item in interiors)
                        {
                            if (item.Stat.Equals(EStats.Strength)) modifier += item.Boost;
                        }
                        avatar.Strength += (int) Math.Round(modifier * actType.Value, MidpointRounding.AwayFromZero);
                        break;
                }

                avatar.Stress += act.StressGain;
                avatar.Stamina -= act.StaminaDrain;

                var avatarsTasks = (await _bll.AvatarsTasks.GetWithoutActivitiesByAvatarId(avatar.Id))
                    .GroupBy(e => e.Id)
                    .Select(e => e.First())
                    .ToList();
                
                foreach (var task in avatarsTasks)
                {
                    if (task.TaskQuest.ActivityId.Equals(aa.ActivityId))
                    {
                        task.CurrentState++;
                        task.TaskQuest = null!; // Cannot update 1 entity 3 times. So it must not be committed. Used as readonly so no troubles.
                        _bll.AvatarsTasks.Update(task);
                    }
                }

                var log = new Logs()
                {
                    AvatarId = avatar.Id,
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Activity completed! " + aa.Id.ToString()
                };
                _bll.Logs.Add(log);
                
                _bll.Avatar.Update(avatar);
                await _bll.AvatarsActivity.RemoveAsync(aa);
                await _bll.SaveChangesAsync();
                return true;
            }

            return false;
        }

        // // GET: Activity/Details/5
        // public async Task<IActionResult> Details(Guid? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var activity = await _bll.Activity
        //         .FirstOrDefaultAsync(id.Value);
        //     if (activity == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(activity);
        // }

        // GET: Activity/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ActivityCreateEditeViewModel()
            {
                Types = new SelectList(await _bll.ActivityType.GetAllAsync(), "Id", "Name"),
                Stats = new SelectList(Enum.GetValues(typeof(EStats)))
            };
            return View(vm);
        }

        // POST: Activity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityCreateEditeViewModel vm)
        {
            var avatar = (await _bll.Avatar.GetByUserId(((await _userManager.GetUserAsync(User))!).Id)).First(e => e.IsActive);
            if (ModelState.IsValid)
            {
                // var type = _bll.ActivityType.GetById(vm.Activity.ActivityTypeId);
                // vm.Activity.ActivityType = type;
                _bll.Activity.Add(vm.Activity);
                _bll.Logs.Add(new Logs()
                {
                    AvatarId = avatar.Id,
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "New activity created! " + vm.Activity.ToString()
                });
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Types = new SelectList(await _bll.ActivityType.GetAllAsync(), "Id", "Name");
            vm.Stats = new SelectList(Enum.GetValues(typeof(EStats)));
            return View(vm);
        }

        // GET: Activity/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _bll.Activity.FirstOrDefaultAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }
            var vm = new ActivityCreateEditeViewModel()
            {
                Types = new SelectList(await _bll.ActivityType.GetAllAsync(), nameof(ActivityType.Name)),
                Stats = new SelectList(Enum.GetValues(typeof(EStats)))
            };
            return View(vm);
        }

        // POST: Activity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ActivityCreateEditeViewModel vm)
        {
            if (id != vm.Activity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Activity.Update(vm.Activity);
                    _bll.Logs.Add(new Logs()
                    {
                        Time = DateTime.Now.ToUniversalTime(),
                        Message = "Activity updated! " + vm.Activity.ToString(),
                    });
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(vm.Activity.Id))
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
            vm.Types = new SelectList(await _bll.ActivityType.GetAllAsync(), nameof(ActivityType.Name));
            vm.Stats = new SelectList(Enum.GetValues(typeof(EStats)));
            return View(vm);
        }

        // GET: Activity/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _bll.Activity
                .FirstOrDefaultAsync(id.Value);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activity = await _bll.Activity.FirstOrDefaultAsync(id);
            if (activity != null)
            {
                await _bll.Activity.RemoveAsync(activity);
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Activity deleted! " + activity.ToString()
                });
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(Guid id)
        {
            return _bll.Activity.Exists(id);
        }
    }
}
