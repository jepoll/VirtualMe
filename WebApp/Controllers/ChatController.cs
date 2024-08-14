using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ICoreBLL _bll;

        public ChatController(ICoreBLL bll)
        {
            _bll = bll;
        }

        // GET: Chat
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Chat.GetAllAsync());
        }

        // GET: Chat/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _bll.Chat
                .FirstOrDefaultAsync(id.Value);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }
        

        // GET: Chat/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _bll.Chat
                .FirstOrDefaultAsync(id.Value);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // POST: Chat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var chat = await _bll.Chat.FirstOrDefaultAsync(id);
            if (chat != null)
            {
                _bll.Logs.Add(new Logs()
                {
                    Time = DateTime.Now.ToUniversalTime(),
                    Message = "Chat deleted " + chat.ToString()
                });
                await _bll.Chat.RemoveAsync(chat);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatExists(Guid id)
        {
            return _bll.Chat.Exists(id);
        }
    }
}
