using Core.Domain.AddressTables;
using DALDTOAT = Core.DAL.DTO.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.AddressTables;

public interface IAvatarsActivityRepository : IEntityRepository<DALDTOAT.AvatarsActivity>, IAvatarsActivityRepositoryShared<DALDTOAT.AvatarsActivity>
{
    // public IEnumerable<AvatarsActivity> GetWithData();
    // public Task<IEnumerable<AvatarsActivity>> GetWithDataAsync();
}

public interface IAvatarsActivityRepositoryShared<TEntity>
{
    public IEnumerable<TEntity> GetWithData();
    public Task<IEnumerable<TEntity>> GetWithDataAsync();
    public TEntity? GetByAvatarId(Guid id);
}
