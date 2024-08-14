using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface IInteriorRepository : IEntityRepository<DALDTOE.Interior>, IInteriorRepositoryShared<DALDTOE.Interior>
{
    
}

public interface IInteriorRepositoryShared<TEntity>
{
    TEntity GetById(Guid id);
}