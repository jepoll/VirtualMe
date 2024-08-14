using DALDTOAT = Core.DAL.DTO.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.AddressTables;

public interface IAvatarOwnsInteriorRepository : IEntityRepository<DALDTOAT.AvatarOwnsInterior>, IAvatarOwnsInteriorRepositoryShared<DALDTOAT.AvatarOwnsInterior>
{
    //Get
    IEnumerable<DALDTOAT.AvatarOwnsInterior> GetAllByAvatarsId(Guid id);
    IEnumerable<DALDTOAT.AvatarOwnsInterior> GetAllByInteriorId(Guid id);
    new bool Exists(Guid avatarId, Guid interiorId);
    
    //GetAsync
    // Task<IEnumerable<DALDTOAT.AvatarOwnsInterior>> GetAllByAvatarsIdAsync(Guid id);
    // Task<IEnumerable<DALDTOAT.AvatarOwnsInterior>> GetAllByInteriorIdAsync(Guid id);
    new Task<bool> ExistsAsync(Guid avatarId, Guid interiorId);
}

public interface IAvatarOwnsInteriorRepositoryShared<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByAvatarsIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllByInteriorIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllByAvatarIdWithInteriorAsync(Guid id);
}