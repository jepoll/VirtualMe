using Shared.Contracts.DAL;
using Shared.DAL.EF;
using Shared.Test.Domain;

namespace Shared.Test.DAL;

public class TestEntityRepository : BaseEntityRepository<TestEntity, TestEntity, TestDbContext>
{
    public TestEntityRepository(TestDbContext dbContext, IDalMapper<TestEntity, TestEntity> mapper) : base(dbContext, mapper)
    {
    }
}
