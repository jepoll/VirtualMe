using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.DAL.EF;
using Core.Domain.Entities;
using Logs = Core.BLL.DTO.Entities.Logs;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LogsController : Controller
    {
        private readonly ICoreBLL _bll;

        public LogsController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/Logs
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Logs.GetAllAsync());
        }

        // GET: Admin/Logs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logs = await _bll.Logs
                .FirstOrDefaultAsync(id.Value);
            if (logs == null)
            {
                return NotFound();
            }

            return View(logs);
        }

        // GET: Admin/Logs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvatarId,Time,Message,Id")] Core.BLL.DTO.Entities.Logs logs)
        {
            if (ModelState.IsValid)
            {
                logs.Id = Guid.NewGuid();
                _bll.Logs.Add(logs);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logs);
        }

        // GET: Admin/Logs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logs = await _bll.Logs.FirstOrDefaultAsync(id.Value);
            if (logs == null)
            {
                return NotFound();
            }
            return View(logs);
        }

        // POST: Admin/Logs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AvatarId,Time,Message,Id")] Logs logs)
        {
            if (id != logs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Logs.Update(logs);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogsExists(logs.Id))
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
            return View(logs);
        }

        // GET: Admin/Logs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logs = await _bll.Logs
                .FirstOrDefaultAsync(id.Value);
            if (logs == null)
            {
                return NotFound();
            }

            return View(logs);
        }

        // POST: Admin/Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var logs = await _bll.Logs.FirstOrDefaultAsync(id);
            if (logs != null)
            {
                await _bll.Logs.RemoveAsync(logs);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogsExists(Guid id)
        {
            return _bll.Logs.Exists(id);
        }
    }
}
