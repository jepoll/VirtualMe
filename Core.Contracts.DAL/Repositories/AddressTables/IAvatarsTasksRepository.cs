using DALDTOAT = Core.DAL.DTO.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.AddressTables;

public interface IAvatarsTasksRepository : IEntityRepository<DALDTOAT.AvatarsTasks>, IAvatarsTasksRepositoryShared<DALDTOAT.AvatarsTasks>
{
    //TODO: custom methods
}

public interface IAvatarsTasksRepositoryShared<TEntity>
{
    public Task<IEnumerable<TEntity>> GetWithDataAsync();
    public Task<IEnumerable<TEntity>> GetByAvatarIdWithDataAsync(Guid id);

    public Task<IEnumerable<Guid>> GetOnlyIdsByAvatarId(Guid id);
    public Task<IEnumerable<TEntity>> GetWithoutActivitiesByAvatarId(Guid id);
}