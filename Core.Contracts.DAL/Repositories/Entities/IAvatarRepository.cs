using Core.Resources.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface IAvatarRepository : IEntityRepository<DALDTOE.Avatar>, IAvatarRepositoryShared<DALDTOE.Avatar>
{
    public IEnumerable<DALDTOE.Avatar> GetAvatarsWithUsers();
    public Task<IEnumerable<DALDTOE.Avatar>> GetAvatarsWithUsersAsync();
}

public interface IAvatarRepositoryShared<TEntity>
{
    public IEnumerable<TEntity> GetAvatarsWithUsers();
    public Task<IEnumerable<TEntity>> GetAvatarsWithUsersAsync();
    public TEntity? GetById(Guid id);
    public Task<IEnumerable<TEntity>> GetByUserId(Guid id);

    public Task<TEntity?> GetAvatarUpdatedByUserId(Guid id);
}