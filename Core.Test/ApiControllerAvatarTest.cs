using AutoMapper;
using Core.BLL;
using Core.Contracts.BLL;
using Core.Contracts.DAL;
using Core.DAL.EF;
using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using WebApp.APIControllers;

namespace Core.Test;

public class ApiControllerAvatarTest
{
    private AppDbContext _ctx;

    private ICoreBLL _bll;

    private ICoreUnitOfWork _uow;

    private UserManager<AppUser> _userManager;

    // sut
    private AvatarController _controller;

    public ApiControllerAvatarTest()
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

    [Fact]
    public async Task GetTest()
    {
        var result = await _controller.GetAll();
        var okRes = result.Result as OkObjectResult;
        var val = okRes!.Value as List<Core.DTO.v1_0.Entities.Avatar>;
        Assert.Empty(val);
        Console.WriteLine(result);
    }

}