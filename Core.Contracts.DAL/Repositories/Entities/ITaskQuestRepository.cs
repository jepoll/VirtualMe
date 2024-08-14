using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface ITaskQuestRepository : IEntityRepository<DALDTOE.TaskQuest>, ITaskQuestRepositoryShared<DALDTOE.TaskQuest>
{
    //TODO: custom methods
}

public interface ITaskQuestRepositoryShared<TEntity>
{
    public Task<IEnumerable<TEntity>> GetWithDataAsync();
    public TEntity GetByIdWithData(Guid id);
}