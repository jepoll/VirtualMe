using System.Diagnostics.Contracts;
using AutoMapper;
using Core.BLL;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Core.Domain.Identity;
using Core.BLL.DTO.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mono.TextTemplating;
using NSubstitute;
using WebApp.APIControllers;

namespace Core.Test;

public class AvatarRepositoryAndServiceTests
{
   private readonly AppDbContext _ctx;
   private readonly ICoreBLL _bll;
   private readonly ICoreUnitOfWork _uow;
   private readonly UserManager<AppUser> _userManager;
   private readonly AvatarController _controller;

   private IAvatarRepository _avatarRepository;

   public AvatarRepositoryAndServiceTests()
   {
      // set up mock database - inmemory
      var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

      // use random guid as db instance id
      optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

      _ctx = new AppDbContext(optionsBuilder.Options);

      var configUow =
         new MapperConfiguration(cfg => cfg.CreateMap<Core.Domain.Entities.Avatar, DAL.DTO.Entities.Avatar>().ReverseMap());
      var mapperUow = configUow.CreateMapper();


      _uow = new CoreUOW(_ctx, mapperUow);
      _avatarRepository = _uow.Avatars;

      var configBll = new MapperConfiguration(cfg => cfg.CreateMap<DAL.DTO.Entities.Avatar, BLL.DTO.Entities.Avatar>().ReverseMap());
      var mapperBll = configBll.CreateMapper();
      _bll = new CoreBLL(_uow, mapperBll);

      var configWeb = new MapperConfiguration(cfg => cfg.CreateMap<BLL.DTO.Entities.Avatar, DTO.v1_0.Entities.Avatar>().ReverseMap());
      var mapperWeb = configWeb.CreateMapper();


      var storeStub = Substitute.For<IUserStore<AppUser>>();
      var optionsStub = Substitute.For<IOptions<IdentityOptions>>();
      var hasherStub = Substitute.For<IPasswordHasher<AppUser>>();

      var validatorStub = Substitute.For<IEnumerable<IUserValidator<AppUser>>>();
      var passwordStup = Substitute.For<IEnumerable<IPasswordValidator<AppUser>>>();
      var lookupStub = Substitute.For<ILookupNormalizer>();
      var errorStub = Substitute.For<IdentityErrorDescriber>();
      var serviceStub = Substitute.For<IServiceProvider>();
      var loggerStub = Substitute.For<ILogger<UserManager<AppUser>>>();

      _userManager = Substitute.For<UserManager<AppUser>>(
         storeStub, 
         optionsStub, 
         hasherStub,
         validatorStub, passwordStup, lookupStub, errorStub, serviceStub, loggerStub
      );
      
      _controller = new AvatarController(_bll, mapperWeb, _userManager);
      _userManager.GetUserId(_controller.User).Returns(Guid.NewGuid().ToString());

   }
   
   // Tests for repo

   [Fact]
   public async Task CreateUser()
   {
      var userId = _userManager.GetUserId(_controller.User);
      _ctx.Users.Add(new AppUser()
      {
         Id = Guid.Parse(userId),
         NickName = "Admin",
         Email = "admin@admin.ee",
         UserName = "admin@admin.ee",
      });
      await _ctx.SaveChangesAsync();
      Assert.NotEmpty(_ctx.Users);
   }
   
   [Fact]
   public async void CreateAvatar()
   {
      await CreateUser();
      var user = _ctx.Users.First();
      var avatar = new Avatar()
      {
         AppUserId = user.Id,
         IsActive = true ,
         LastChanges = DateTime.Now.ToUniversalTime()
      };
      _bll.Avatar.Add(avatar);
      await _bll.SaveChangesAsync();
      //Untrack created entity. Important for future gets
      _ctx.ChangeTracker.Clear();      
      Assert.NotEmpty(await _bll.Avatar.GetAllAsync());
   }

   [Fact]
   public async Task UpdateAvatar()
   {
      var userId = _userManager.GetUserId(_controller.User);
      CreateAvatar();
      var avatar = (await _bll.Avatar.GetByUserId(Guid.Parse(userId))).FirstOrDefault(e => e.IsActive);
      avatar.Money += 1000;
      
      _bll.Avatar.Update(avatar);
      await _bll.SaveChangesAsync();
      
      var changedAvatar = (await _bll.Avatar.GetByUserId(Guid.Parse(userId))).FirstOrDefault(e => e.IsActive);
      
      Assert.True(changedAvatar!.Money >= 1000);
   }

   [Fact]
   public async Task GetByUserId()
   {
      var userId = _userManager.GetUserId(_controller.User);
      CreateAvatar();
      var avatar = (await _bll.Avatar.GetByUserId(Guid.Parse(userId))).FirstOrDefault(e => e.IsActive);
      Assert.NotNull(avatar);
   }

   [Fact]
   public async Task OtherSmallTests()
   {
      var userId = _userManager.GetUserId(_controller.User);
      CreateAvatar();
      var avatars = _bll.Avatar.GetAll(Guid.Parse(userId));
      Assert.NotEmpty(avatars);
   }

   [Fact]
   public async Task GetByIds()
   {
      var userId = _userManager.GetUserId(_controller.User);
      CreateAvatar();
      var avatar = (await _bll.Avatar.GetByUserId(Guid.Parse(userId))).First(e => e.IsActive);
      var avatar2 = _bll.Avatar.GetById(avatar.Id);
      
      Assert.Equal(avatar.Id, avatar2.Id);

      avatar = _bll.Avatar.FirstOrDefault(avatar.Id);
      avatar2 = await _bll.Avatar.FirstOrDefaultAsync(avatar2.Id);
      Assert.Equal(avatar.Id, avatar2.Id);
   }

   [Fact]
   public async Task Exists()
   {
      var userId = _userManager.GetUserId(_controller.User);
      CreateAvatar();
      var avatar = (await _bll.Avatar.GetByUserId(Guid.Parse(userId))).First(e => e.IsActive);
      var exists = _avatarRepository.Exists(avatar.Id);
      Assert.True(exists);
      var existsAsync = await _avatarRepository.ExistsAsync(avatar.Id);
      Assert.True(existsAsync);
   }

   [Fact]
   public async Task GetAvatarsWithUsers()
   {
      var userId = _userManager.GetUserId(_controller.User);
      CreateAvatar();
      var avatars = _bll.Avatar.GetAvatarsWithUsers();
      Assert.NotNull(avatars.First().AppUser);
      var avatarsAsync = await _bll.Avatar.GetAvatarsWithUsersAsync();
      Assert.NotNull(avatarsAsync.First().AppUser);
   }

   [Fact]
   public async Task GetUpdatedAvatar()
   {
      var userId = _userManager.GetUserId(_controller.User);
      CreateAvatar();
      var avatar = await _bll.Avatar.GetAvatarUpdatedByUserId(Guid.Parse(userId!));
      Assert.NotNull(avatar);

      avatar.Health = 101;
      avatar.Stamina += 150;
      avatar.Stress = 100;
      avatar.Hunger = 100;
      avatar.Exp += 200;
      _bll.Avatar.Update(avatar);
      await _bll.SaveChangesAsync();
      _ctx.ChangeTracker.Clear();
      avatar = await _bll.Avatar.GetAvatarUpdatedByUserId(Guid.Parse(userId!));
      Assert.True(avatar!.Level >= 1);

      avatar.Hunger = -1;
      avatar.Health = -1;
      _bll.Avatar.Update(avatar);
      await _bll.SaveChangesAsync();
      _ctx.ChangeTracker.Clear();
      avatar = await _bll.Avatar.GetAvatarUpdatedByUserId(Guid.Parse(userId!));
      Assert.True(!avatar!.IsActive);

      avatar = await _bll.Avatar.GetAvatarUpdatedByUserId(Guid.NewGuid());
      Assert.Null(avatar);
   }

   // [Fact]
   // public async Task Remove()
   // {
   //    var userId = _userManager.GetUserId(_controller.User);
   //    CreateAvatar();
   //    var avatar = await _bll.Avatar.GetAvatarUpdatedByUserId(Guid.Parse(userId!));
   //    _ctx.ChangeTracker.Clear();
   //    _bll.Avatar.Remove(avatar);
   //    await _bll.SaveChangesAsync();
   //    Assert.Empty(await _bll.Avatar.GetAllAsync());
   // }
   //
}