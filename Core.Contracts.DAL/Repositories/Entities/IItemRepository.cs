using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface IItemRepository : IEntityRepository<DALDTOE.Item>
{
    DALDTOE.Item GetItemByIdValue(string id);
}

public interface IItemRepositoryShared<TEntity>
{
    TEntity GetItemByIdValue(string id);
}