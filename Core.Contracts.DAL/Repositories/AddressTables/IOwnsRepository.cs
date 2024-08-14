using DALDTOAT = Core.DAL.DTO.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.AddressTables;

public interface IOwnsRepository : IEntityRepository<DALDTOAT.Owns>, IOwnsRepositoryShared<DALDTOAT.Owns>
{
    //TODO: custom methods
}

public interface IOwnsRepositoryShared<TEntity>
{
    public IEnumerable<TEntity> GetAllByAvatarId(Guid avatarId);
    public Task<IEnumerable<TEntity>> GetAllByAvatarIdAsync(Guid avatarId);

    public TEntity GetByAvatarAndItemIds(Guid avatarId, Guid itemId);
    public TEntity GetByIdString(String id);
    public Task<IEnumerable<TEntity>> GetAllWIthDataAsync();
    public Task<TEntity> GetByIdWithData(Guid id);
}