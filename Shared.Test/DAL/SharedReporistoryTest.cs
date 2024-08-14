using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.DAL.EF;
using Shared.Test.Domain;

namespace Shared.Test.DAL;

public class SharedReporistoryTest
{
    private readonly TestDbContext _ctx;
    private readonly TestEntityRepository _testEntityRepository;

    public SharedReporistoryTest()
    {
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new TestDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<TestEntity, TestEntity>());
        var mapper = config.CreateMapper();

        _testEntityRepository =
            new TestEntityRepository(
                _ctx,
                new BaseDalDomainMapper<TestEntity, TestEntity>(mapper)
            );
    }


    [Fact]
    public async Task Test1()
    {
        // arrange
        _testEntityRepository.Add(new TestEntity() {Message = "Foo", Time = DateTime.Now});
        _ctx.SaveChanges();

        // act
        var data = await _testEntityRepository.GetAllAsync();

        // assert
        Assert.Equal(1, data.Count());
    }

}