using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface IChatRepository : IEntityRepository<DALDTOE.Chat>
{
    new bool Exists(Guid avatarId, Guid interiorId);
    new Task<bool> ExistsAsync(Guid avatarId, Guid interiorId);
}