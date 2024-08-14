using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface IActivityTypeReposirtory : IEntityRepository<DALDTOE.ActivityType>
{
    public DALDTOE.ActivityType GetById(Guid id);
}

public interface IActivityTypeRepositoryShared<TEntity>
{
    public TEntity GetById(Guid id);
}