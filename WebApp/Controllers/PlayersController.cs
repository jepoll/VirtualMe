﻿using System.Text.RegularExpressions;
using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.BLL;
using WebApp.ViewModels;
using Avatar = Core.BLL.DTO.Entities.Avatar;
using Chat = Core.BLL.DTO.Entities.Chat;

namespace WebApp.Controllers;

public class PlayersController : BaseController
{
    private readonly ICoreBLL _bll;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICoreUnitOfWork _uow;

        public PlayersController(ICoreBLL bll, UserManager<AppUser> userManager, ICoreUnitOfWork uow)
        {
            _bll = bll;
            _userManager = userManager;
            _uow = uow;
        }

        // GET: Owns
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var avatars = await _bll.Avatar.GetAllAsync();

            var avatarsWithUsers = new AvatarsWithUserViewModel();

            foreach (var avatar in avatars)
            {
                var appUser = await _userManager.FindByIdAsync(avatar.AppUserId.ToString());
                if (avatar.IsActive && avatar.AppUserId != user!.Id)
                {
                    avatarsWithUsers.Avatars.Add(avatar);
                    avatarsWithUsers.AppUsers.Add(appUser);
                }
            }
            
            // var currentAvatar = avatars.FirstOrDefault(a => a.AppUserId == user!.Id);
            // var filteredAvatars = avatarsWithUsers.Where(a => a.Avatar.Id != currentAvatar!.Id && a.Avatar.IsActive);
            
            return View(avatarsWithUsers);

        }

        // GET: Owns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _bll.Avatar
                .FirstOrDefaultAsync(id.Value);
            if (avatar == null)
            {
                return NotFound();
            }

            return View(avatar);
        }

        public async Task<IActionResult> Chat(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var avatar = await _bll.Avatar
                .FirstOrDefaultAsync(id.Value);
            if (avatar == null)
            {
                return NotFound();
            }
            
            var users = await _uow.Users.GetAllAsync();
            var currentUser = await _userManager.GetUserAsync(User);
            var otherUser = users.FirstOrDefault(user => user.Id.Equals(avatar.AppUserId));
            var avatars = await _bll.Avatar.GetAllAsync();
            var currentAvatar = avatars.FirstOrDefault(avatar => avatar.AppUserId.Equals(currentUser!.Id));
            
            var chat = new Chat
            {
                Id = new Guid(),
                // Avatar1 = currentAvatar!,
                // Avatar2 = avatar,
                Avatar1Id = currentAvatar!.Id,
                Avatar2Id = avatar.Id,
                FirstAvatarsName = currentUser!.UserName!.ToString(),
                SecondAvatarsName = otherUser!.UserName!.ToString()
            };

            if (await _bll.Chat.ExistsAsync(avatar.Id, currentAvatar.Id))
            {
                return RedirectToAction("Index", "Chat");
            }
            
            _bll.Logs.Add(new Logs()
            {
                Time = DateTime.Now.ToUniversalTime(),
                Message = "New chat created! " + chat.ToString()
            });
            _bll.Chat.Add(chat);
            // _bll.Avatar.Update(avatar);
            // _bll.Avatar.Update(currentAvatar);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Index", "Chat");
        }

        private bool AvatarExists(Guid id)
        {
            return _bll.Avatar.Exists(id);
        }
    
}