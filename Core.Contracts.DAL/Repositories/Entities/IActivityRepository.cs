using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface IActivityRepository : IEntityRepository<DALDTOE.Activity>
{
    DALDTOE.Activity GetById(Guid id);
}

public interface IActivityRepositoryShared<TEntity>
{
    TEntity GetById(Guid id);
}