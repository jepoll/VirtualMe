using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class MessageController : BaseController
    {
        private readonly ICoreBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(UserManager<AppUser> userManager, ICoreBLL bll)
        {
            _userManager = userManager;
            _bll = bll;
        }

        public IActionResult ChatBox(Guid id)
        {
            var vm = new MessagesCreateViewModel
            {
                Messages = _bll.Message.GetAllMessages(id)
            };
            return View(vm);
        }

        // GET: Message
        public async Task<IActionResult> Index(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new MessagesCreateViewModel
            {
                Messages = _bll.Message.GetAllMessages(id),
                ChatId = id,
                UserName = user!.UserName! + ": "
            };
            return View(vm);
        }

        public async Task<IActionResult> SendMessage(MessagesCreateViewModel vm)
        {
            vm.Message.SendingDate = DateTime.Now.ToUniversalTime();
            //var chat = await _uow.Chats.FirstOrDefaultAsync(vm.Message.ChatId);
            if (ModelState.IsValid)
            {
                // _bll.Logs.Add(new Logs()
                // {
                //     Time = DateTime.Now,
                //     Message = "New message created! " + vm.Message.ToString()
                // });
                _bll.Message.Add(vm.Message);
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { id = vm.ChatId});
        }
    }
}
