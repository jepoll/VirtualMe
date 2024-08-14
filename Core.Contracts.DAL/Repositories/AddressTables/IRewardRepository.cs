using DALDTOAT = Core.DAL.DTO.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.AddressTables;

public interface IRewardRepository : IEntityRepository<DALDTOAT.Reward>, IRewardRepositoryShared<DALDTOAT.Reward>
{
    //TODO: custom methods
}

public interface IRewardRepositoryShared<TEntity>
{
    public Task<IEnumerable<TEntity>> GetWithDataAsync();
    public TEntity GetByTaskQuestId(Guid id);
}